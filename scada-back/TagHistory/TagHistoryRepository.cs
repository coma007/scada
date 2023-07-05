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
        return (await _tagRecords.FindAsync(tagRecord => true)).ToList();
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetAll(string tagName)
    {
        return (await _tagRecords.FindAsync(tagRecord => tagRecord.TagName == tagName)).ToList();
    }

    public async Task<IEnumerable<TagHistoryRecord>> GetBetween(DateTime startDateTime, DateTime endDateTime)
    {
        var filter = Builders<TagHistoryRecord>.Filter.And(
            Builders<TagHistoryRecord>.Filter.Gte(record => record.Timestamp, startDateTime),
            Builders<TagHistoryRecord>.Filter.Lt(record => record.Timestamp, endDateTime)
        );
        return (await _tagRecords.FindAsync(filter)).ToList();
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