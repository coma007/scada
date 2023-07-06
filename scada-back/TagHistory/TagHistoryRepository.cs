using MongoDB.Driver;
using scada_back.Database;
using scada_back.Exception;

namespace scada_back.TagHistory;

public class TagHistoryRepository : ITagHistoryRepository
{
    private IMongoCollection<TagHistoryRecord> _tagRecords;

    public TagHistoryRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _tagRecords = database.GetCollection<TagHistoryRecord>(settings.TagsHistoryCollectionName);

    }
    public async Task<IEnumerable<TagHistoryRecord>> GetAll()
    {
        return (await _tagRecords.Find(Builders<TagHistoryRecord>.Filter.Empty)
            .SortByDescending(record => record.Timestamp).ToListAsync());
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetAll(string tagName)
    {
        return (await _tagRecords.Find(tagRecord => tagRecord.TagName == tagName)
            .SortByDescending(record => record.TagValue).ToListAsync());
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetBetween(DateTime startDateTime, DateTime endDateTime)
    {
        var filter = Builders<TagHistoryRecord>.Filter.And(
            Builders<TagHistoryRecord>.Filter.Gte(record => record.Timestamp, startDateTime),
            Builders<TagHistoryRecord>.Filter.Lt(record => record.Timestamp, endDateTime)
        );
        return await _tagRecords.Find(filter).SortByDescending(record => record.Timestamp).ToListAsync();
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetLast(IEnumerable<string> tagNames)
    {
        var filter = Builders<TagHistoryRecord>.Filter.In(record => record.TagName, tagNames);
        return await _tagRecords.Find(filter).SortByDescending(record => record.Timestamp).ToListAsync();
    }

    public async void Create(TagHistoryRecord newRecord)
    {
        try
        {
            await _tagRecords.InsertOneAsync(newRecord);
        }
        catch (System.Exception e)
        {
            throw new ActionNotExecutedException("Create failed.");
        }
    }
}