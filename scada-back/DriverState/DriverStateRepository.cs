using MongoDB.Driver;
using scada_back.Database;
using scada_back.Exception;

namespace scada_back.DriverState;

public class DriverStateRepository : IDriverStateRepository
{
    private IMongoCollection<DriverState> _states;
    private FilterDefinition<DriverState> empty = Builders<DriverState>.Filter.Empty;

    public DriverStateRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _states = _database.GetCollection<DriverState>(settings.DriversStateCollectionName);
    }
    public async Task<IEnumerable<DriverState>> GetAll()
    {
        return await _states.Find(empty).ToListAsync(); 
    }

    public async Task<DriverState> Get(int dtoIoAddress)
    {
        return await _states
            .Find(Builders<DriverState>.Filter.Eq(x => x.IOAddress, dtoIoAddress))
            .FirstOrDefaultAsync();
    }

    public async Task<DriverState> Create(DriverState state)
    {
        await _states.InsertOneAsync(state);
        DriverState newState = await Get(state.IOAddress);
        if (newState == null)
        {
            throw new ActionNotExecutedException("Create failed.");
        }
        return newState;
    }

    public async Task<DriverState> Update(DriverState updatedState)
    {
        DriverState oldState = Get(updatedState.IOAddress).Result;
        if (oldState == null)
            throw new ObjectNotFoundException($"Driver state with address {updatedState.IOAddress} doesn't exist");
        updatedState.Id = oldState.Id;
        ReplaceOneResult result = await _states.ReplaceOneAsync(state => state.IOAddress == updatedState.IOAddress, updatedState);
        if (result.ModifiedCount == 0)
        {
            throw new ActionNotExecutedException("Update failed.");
        }

        return await Get(updatedState.IOAddress);
    }
}