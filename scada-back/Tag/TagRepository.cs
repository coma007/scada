using MongoDB.Bson;
using MongoDB.Driver;
using scada_back.Alarm;
using scada_back.Database;
using scada_back.Exception;
using scada_back.TagHistory;
using System.Security.Claims;

namespace scada_back.Tag;

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
            //Model.Abstraction.Tag tag = await GetDeleted(tagName);
            if (result.DeletedCount == 0 || toBeDeleted == null)
            {
                throw new ActionNotExecutedException("Delete failed.");
            }

            List<Alarm.Alarm> alarms = (await _alarms.FindAsync(alarm => alarm.TagName == toBeDeleted.TagName))
                .ToListAsync().Result;
            if (alarms.Count > 0)
            {
                IEnumerable<string> alarmNames = alarms.Select(alarms => alarms.AlarmName);
                int noOfAlarms = alarms.Count;
                var filter = Builders<Alarm.Alarm>.Filter.In(record => record.AlarmName, alarmNames);
                result = await _alarms.DeleteManyAsync(session, filter);
                await _deletedAlarms.InsertManyAsync(session, alarms);
                //alarms = (await _deletedAlarms.FindAsync(alarm => alarm.TagName == toBeDeleted.TagName)).ToListAsync().Result;

                if (result.DeletedCount < noOfAlarms || alarms.Count < noOfAlarms)
                {
                    await session.AbortTransactionAsync();
                    throw new ActionNotExecutedException("Delete failed.");
                }
            }

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