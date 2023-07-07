using MongoDB.Driver;
using scada_back.Database;
using scada_back.Exception;

namespace scada_back.Alarm;

public class AlarmRepository: IAlarmRepository
{
    private readonly IMongoCollection<Alarm> _alarms;
    private readonly IMongoCollection<Alarm> _deletedAlarms;
    private readonly IMongoClient _client;

    public AlarmRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        _client = mongoClient;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _alarms = database.GetCollection<Alarm>(settings.AlarmsCollectionName);
        _deletedAlarms = database.GetCollection<Alarm>(settings.AlarmsDeletedCollectionName);
    }
    
    public async Task<IEnumerable<Alarm>> GetAll()
    {
        return (await _alarms.FindAsync(alarm => true)).ToList();
    }
    
    public async Task<Alarm> Get(string alarmName)
    {
        return (await _alarms
                .FindAsync(alarm => alarm.AlarmName == alarmName))
            .FirstOrDefault();
    }

    private async Task<Alarm> GetDeleted(string alarmName)
    {
        return (await _deletedAlarms
            .FindAsync(alarm => alarm.AlarmName == alarmName)).FirstOrDefault();
    }

    public async Task<Alarm> Create(Alarm newAlarm)
    {
        await _alarms.InsertOneAsync(newAlarm);
        Alarm alarm = await Get(newAlarm.AlarmName);
        if (alarm == null)
        {
            throw new ActionNotExecutedException("Create failed.");
        }
        return alarm;
    }
    
    public async Task<Alarm> Delete(string alarmName)
    {
        using var session = await _client.StartSessionAsync();
        session.StartTransaction();
        try
        {
            Alarm toBeDeleted = await Get(alarmName);
            if (toBeDeleted == null)
            {
                throw new ObjectNotFoundException($"Alarm with {alarmName} doesn't exist");
            }

            DeleteResult result = await _alarms.DeleteOneAsync(session, alarm => alarm.AlarmName == alarmName);
            await _deletedAlarms.InsertOneAsync(session, toBeDeleted);
            //Alarm alarm = await GetDeleted(alarmName);
            if (result.DeletedCount == 0 || toBeDeleted == null)
            {
                throw new ActionNotExecutedException("Deletion failed.");
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

    public async Task<Alarm> Update(Alarm updatedAlarm)
    {
        Alarm oldAlarm = Get(updatedAlarm.AlarmName).Result;
        if (oldAlarm == null) {
            throw new ObjectNotFoundException($"Alarm with {updatedAlarm.AlarmName} doesn't exist");
        }
        updatedAlarm.Id = oldAlarm.Id;
        ReplaceOneResult result = await _alarms.ReplaceOneAsync(alarm => alarm.AlarmName == updatedAlarm.AlarmName, updatedAlarm);
        if (result.ModifiedCount == 0)
        {
            throw new ActionNotExecutedException("Update failed.");
        }
        return await Get(updatedAlarm.AlarmName);
    }
}