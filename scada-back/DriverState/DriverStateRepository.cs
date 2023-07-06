using MongoDB.Driver;
using scada_back.Database;
using scada_back.Exception;

namespace scada_back.DriverState;

public class DriverStateRepository : IDriverStateRepository
{
    private IMongoCollection<DriverState> _states;

    public DriverStateRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _states = database.GetCollection<DriverState>(settings.DriversStateCollectionName);
    }
    public async Task<IEnumerable<DriverState>> GetAll()
    {
        return (await _states.FindAsync(driverState => true)).ToList(); 
    }

    public async Task<DriverState> Get(int ioAddress)
    {
        return await _states
            .Find(Builders<DriverState>.Filter.Eq(x => x.IOAddress, ioAddress))
            .FirstOrDefaultAsync();
    }

    public async Task<DriverState> Create(DriverState driverState)
    {
        await _states.InsertOneAsync(driverState);
        DriverState newState = await Get(driverState.IOAddress);
        if (newState == null)
        {
            throw new ActionNotExecutedException("Create failed.");
        }
        return newState;
    }

    public async Task<DriverState> Update(DriverState driverState)
    {
        DriverState oldState = Get(driverState.IOAddress).Result;
        if (oldState == null)
            throw new ObjectNotFoundException($"Driver state with address {driverState.IOAddress} doesn't exist");
        driverState.Id = oldState.Id;
        ReplaceOneResult result = await _states.ReplaceOneAsync(state => state.IOAddress == driverState.IOAddress, driverState);
        if (result.ModifiedCount == 0)
        {
            throw new ActionNotExecutedException("Update failed.");
        }

        return await Get(driverState.IOAddress);
    }
}