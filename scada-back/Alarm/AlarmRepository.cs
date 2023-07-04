using MongoDB.Driver;
using scada_back.Database;

namespace scada_back.Alarm;

public class AlarmRepository
{
    private IMongoCollection<Alarm> _alarms;

    public AlarmRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _alarms = _database.GetCollection<Alarm>(settings.AlarmsCollectionName);
    }
}