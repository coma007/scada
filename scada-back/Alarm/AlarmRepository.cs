using MongoDB.Driver;
using scada_back.Database;
using scada_back.Exception;

namespace scada_back.Alarm;

public class AlarmRepository: IAlarmRepository
{
    private IMongoCollection<Alarm> _alarms;
    private FilterDefinition<Alarm> empty = Builders<Alarm>.Filter.Empty;

    public AlarmRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _alarms = _database.GetCollection<Alarm>(settings.AlarmsCollectionName);
    }

    public async Task<Alarm> GetByName(string name)
    {
        return await _alarms
            .Find(Builders<Alarm>.Filter.Eq(x => x.AlarmName, name))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Alarm>> GetAll()
    {
        return await _alarms.Find(empty).ToListAsync();
    }

    public async Task<Alarm> Create(Alarm newAlarm)
    {
        await _alarms.InsertOneAsync(newAlarm);
        return newAlarm;
    }

    public async Task<Alarm> Delete(string name)
    {
        Alarm toBeDeleted = await GetByName(name);
        DeleteResult result = await _alarms.DeleteOneAsync(Builders<Alarm>.Filter.Eq(x => x.AlarmName, name));
        if (result.DeletedCount == 0)
        {
            throw new NotExecutedException();
        }
        return toBeDeleted;
    }

    public async Task<Alarm> Update(Alarm toBeUpdated)
    {
        ReplaceOneResult result = await _alarms.ReplaceOneAsync(Builders<Alarm>.Filter.Eq(x => x.AlarmName, toBeUpdated.AlarmName), toBeUpdated);
        if (result.ModifiedCount == 0)
        {
            throw new NotExecutedException();
        }

        return await GetByName(toBeUpdated.AlarmName);
    }
}