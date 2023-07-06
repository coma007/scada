using MongoDB.Driver;
using scada_back.Alarm.Enumeration;
using scada_back.Database;

namespace scada_back.AlarmHistory;

public class AlarmHistoryRecordRepository : IAlarmHistoryRecordRepository
{
    private IMongoCollection<AlarmHistoryRecord> _alarmRecords;
    private IMongoCollection<Alarm.Alarm> _alarms;
    private FilterDefinition<AlarmHistoryRecord> empty = Builders<AlarmHistoryRecord>.Filter.Empty;

    public AlarmHistoryRecordRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _alarmRecords = _database.GetCollection<AlarmHistoryRecord>(settings.AlarmsHistoryCollectionName);
        _alarms = _database.GetCollection<Alarm.Alarm>(settings.AlarmsCollectionName);
    }

    public async Task<IEnumerable<AlarmHistoryRecord>> GetByName(string name)
    {
        return await _alarmRecords
            .Find(Builders<AlarmHistoryRecord>.Filter.Eq(x => x.AlarmName, name))
            .ToListAsync();
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

    public async Task<IEnumerable<AlarmHistoryRecord>> GetBetween(DateTime start, DateTime end)
    {
        var filter = Builders<AlarmHistoryRecord>.Filter.And(
            Builders<AlarmHistoryRecord>.Filter.Gte(a => a.Timestamp, start),
            Builders<AlarmHistoryRecord>.Filter.Lte(a => a.Timestamp, end)
        );

        var alarmRecords = await _alarmRecords.FindAsync(filter).Result.ToListAsync();

        return alarmRecords.Join(
                _alarms.AsQueryable(),
                alarmHistory => alarmHistory.AlarmName,
                alarm => alarm.AlarmName,
                (alarmHistory, alarm) => new { AlarmHistory = alarmHistory, Alarm = alarm })
            .OrderByDescending(a => a.Alarm.AlarmPriority)
            .ThenByDescending(a => a.AlarmHistory.Timestamp)
            .Select(a =>a.AlarmHistory).ToList();
    }

    public async Task<IEnumerable<AlarmHistoryRecord>> GetByPriority(int priority)
    {
        var filteredAlarms = await _alarms
            .Find(Builders<Alarm.Alarm>.Filter.Eq(a => a.AlarmPriority, (AlarmPriority)priority))
            .ToListAsync();

        return filteredAlarms.Join(
                _alarmRecords.AsQueryable(),
                alarms => alarms.AlarmName,
                alarmsHistory => alarmsHistory.AlarmName,
                (alarm, alarmHistory) => new {Alarm = alarm, AlarmHistory = alarmHistory  })
            .OrderByDescending(a => a.AlarmHistory.Timestamp)
            .Select(a =>a.AlarmHistory).ToList();
    }
}