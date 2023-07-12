using MongoDB.Bson;
using MongoDB.Driver;
using scada_back.Infrastructure.Database;
using scada_back.Infrastructure.Exception;

namespace scada_back.Infrastructure.Feature.Tag;

public class TagRepository : ITagRepository
{
    private readonly IMongoCollection<Model.Abstraction.Tag> _tags;
    private readonly IMongoCollection<Model.Abstraction.Tag> _deletedTags;
    private readonly IMongoCollection<Alarm.Alarm> _alarms;
    private readonly IMongoCollection<Alarm.Alarm> _deletedAlarms;
    private readonly IMongoClient _client;

    public TagRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        _client = mongoClient;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _tags = database.GetCollection<Model.Abstraction.Tag>(settings.TagsCollectionName);
        _deletedTags = database.GetCollection<Model.Abstraction.Tag>(settings.TagsDeletedCollectionName);
        _alarms = database.GetCollection<Alarm.Alarm>(settings.AlarmsCollectionName);
        _deletedAlarms = database.GetCollection<Alarm.Alarm>(settings.AlarmsDeletedCollectionName);

    }
    
    public async Task<IEnumerable<Model.Abstraction.Tag>> GetAll()
    {
        return (await _tags.FindAsync(tag => true)).ToList();
    }

    public async Task<IEnumerable<Model.Abstraction.Tag>> GetAll(string discriminator)
    {
        var filter = Builders<Model.Abstraction.Tag>.Filter.Eq("_t", discriminator);
        return (await _tags.FindAsync<Model.Abstraction.Tag>(filter)).ToList();
    }

    public async Task<IEnumerable<string>> GetAllNames(string signalType)
    {
        var filter = Builders<Model.Abstraction.Tag>.Filter.Regex("_t", new BsonRegularExpression($"^{signalType}"));
        IEnumerable<Model.Abstraction.Tag> tags = (await _tags.FindAsync(filter)).ToList();
        return tags.Select(tag => tag.TagName).ToList();
    }

    public async Task<IEnumerable<string>> GetInputScanNames()
    {
        var filter = Builders<Model.Abstraction.Tag>.Filter.And(
            Builders<Model.Abstraction.Tag>.Filter.Regex("_t", new BsonRegularExpression("^(digital_input|analog_input)$")),
            Builders<Model.Abstraction.Tag>.Filter.Eq("scan", true)
        );

        IEnumerable<Model.Abstraction.Tag> tags = await _tags.Find(filter).ToListAsync();
        return tags.Select(tag => tag.TagName);
    }


    public async Task<Model.Abstraction.Tag> Get(string tagName)
    {
        return (await _tags.FindAsync(tag => tag.TagName == tagName)).FirstOrDefault();
    }

    private async Task<Model.Abstraction.Tag> GetDeleted(string tagName)
    {
        return (await _deletedTags.FindAsync(tag => tag.TagName == tagName)).FirstOrDefault();
    }

    public async Task<Model.Abstraction.Tag> Create(Model.Abstraction.Tag newTag)
    {
        await _tags.InsertOneAsync(newTag);
        Model.Abstraction.Tag tag = await Get(newTag.TagName);
        if (tag == null)
        {
            throw new ActionNotExecutedException("Create failed.");
        }
        return tag;
    }

    public async Task<Model.Abstraction.Tag> Delete(string tagName)
    {
        using var session = await _client.StartSessionAsync();
        session.StartTransaction();
        
        Model.Abstraction.Tag toBeDeleted = await Get(tagName);
        try
        {
            if (toBeDeleted == null)
            {
                throw new ObjectNotFoundException($"Tag with {tagName} doesn't exist");
            }
            
            DeleteResult result = await _tags.DeleteOneAsync(session, tag => tag.TagName == tagName);
            await _deletedTags.InsertOneAsync(session, toBeDeleted);

            if (result.DeletedCount == 0)
            {
                throw new ActionNotExecutedException("Delete failed.");
            }

            await DeleteAllAlarms(toBeDeleted, session);

            await session.CommitTransactionAsync();
            return toBeDeleted;
        }
        catch (System.Exception e)
        {
            await session.AbortTransactionAsync();
            switch (e)
            {
                case ObjectNotFoundException:
                    throw new ObjectNotFoundException(e.Message);
                case ActionNotExecutedException:
                    throw new ActionNotExecutedException(e.Message);
                default:
                    throw;
            }
        }
    }

    private async Task DeleteAllAlarms(Model.Abstraction.Tag toBeDeleted, IClientSessionHandle session)
    {
        List<Alarm.Alarm> alarms = (await _alarms.FindAsync(alarm => alarm.TagName == toBeDeleted.TagName))
            .ToListAsync().Result;
        if (alarms.Count > 0)
        {
            IEnumerable<string> alarmNames = alarms.Select(alarms => alarms.AlarmName);
            var filter = Builders<Alarm.Alarm>.Filter.In(record => record.AlarmName, alarmNames);
            DeleteResult result = await _alarms.DeleteManyAsync(session, filter);
            await _deletedAlarms.InsertManyAsync(session, alarms);

            if (result.DeletedCount < alarms.Count)
            {
                await session.AbortTransactionAsync();
                throw new ActionNotExecutedException("Delete failed.");
            }
        }
    }

    public async Task<Model.Abstraction.Tag> Update(Model.Abstraction.Tag updatedTag)
    {
        Model.Abstraction.Tag oldTag = Get(updatedTag.TagName).Result;
        if (oldTag == null) {
            throw new ObjectNotFoundException($"Tag with {updatedTag.TagName} doesn't exist");
        }
        updatedTag.Id = oldTag.Id;
        ReplaceOneResult result = await _tags.ReplaceOneAsync(tag => tag.Id == updatedTag.Id, updatedTag);
        if (result.ModifiedCount == 0)
        {
            throw new ActionNotExecutedException("Update failed.");
        }
        return await Get(updatedTag.TagName);
    }
}