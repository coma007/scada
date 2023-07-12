using MongoDB.Driver;
using scada_back.Infrastructure.Database;
using scada_back.Infrastructure.Exception;

namespace scada_back.Infrastructure.Feature.TagHistory;

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
            .SortByDescending(record => record.Timestamp).ToListAsync());
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetBetween(DateTime startDateTime, DateTime endDateTime)
    {
        var filter = Builders<TagHistoryRecord>.Filter.And(
            Builders<TagHistoryRecord>.Filter.Gte(record => record.Timestamp, startDateTime),
            Builders<TagHistoryRecord>.Filter.Lt(record => record.Timestamp, endDateTime)
        );
        return await _tagRecords.Find(filter).SortByDescending(record => record.Timestamp).ToListAsync();
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetLatest(IEnumerable<string> tagNames)
    {
        var filter = Builders<TagHistoryRecord>.Filter.In(record => record.TagName, tagNames);
        return await _tagRecords.Find(filter).SortByDescending(record => record.Timestamp).ToListAsync();
    }

    public async Task<TagHistoryRecord> GetLastForTag(string tagName)
    {
        var filter = Builders<TagHistoryRecord>.Filter.Eq(record => record.TagName, tagName);
        return await _tagRecords.Find(filter).SortByDescending(record => record.Timestamp).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<TagHistoryRecord>> GetLastForTags(IEnumerable<string> tagNames)
    {
        var filter = Builders<TagHistoryRecord>.Filter.In(record => record.TagName, tagNames);
        var sort = Builders<TagHistoryRecord>.Sort.Descending(record => record.Timestamp);
        var records = await _tagRecords.Find(filter).Sort(sort).ToListAsync();
        var lastRecords = records.GroupBy(record => record.TagName).Select(group => group.First());
        return lastRecords;
    }


    public async void Create(TagHistoryRecord newRecord)
    {
        try
        {
            await _tagRecords.InsertOneAsync(newRecord);
        }
        catch (System.Exception)
        {
            throw new ActionNotExecutedException("Create failed.");
        }
    }
}