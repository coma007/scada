using MongoDB.Driver;
using scada_back.Database;

namespace scada_back.Alarm.AlarmHistory;

public class AlarmHistoryRecordRepository : IAlarmHistoryRecordRepository
{
    private IMongoCollection<AlarmHistoryRecord> _alarmRecords;
    private FilterDefinition<AlarmHistoryRecord> empty = Builders<AlarmHistoryRecord>.Filter.Empty;

    public AlarmHistoryRecordRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _alarmRecords = _database.GetCollection<AlarmHistoryRecord>(settings.AlarmsHistoryCollectionName);
    }

    public async Task<AlarmHistoryRecord> Get(string id)
    {
        return await _alarmRecords
            .Find(Builders<AlarmHistoryRecord>.Filter.Eq(x => x.Id, id))
            .FirstOrDefaultAsync();
    }

    public async Task<AlarmHistoryRecord> Create(AlarmHistoryRecord newRecord)
    {
        await _alarmRecords.InsertOneAsync(newRecord);
        return newRecord;
    }

    public async Task<IEnumerable<AlarmHistoryRecord>> GetAll()
    {
        return await _alarmRecords.Find(empty).ToListAsync(); 
    }
}