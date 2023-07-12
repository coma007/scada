using MongoDB.Driver;
using scada_back.Infrastructure.Database;
using scada_back.Infrastructure.Exception;

namespace scada_back.Infrastructure.Feature.DriverState;

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
        SetId(driverState);
        ReplaceOneResult result = await _states.ReplaceOneAsync(state => state.IOAddress == driverState.IOAddress, driverState);
        // if (result.ModifiedCount == 0)
        // {
        //     throw new ActionNotExecutedException("Update failed.");
        // }

        return await Get(driverState.IOAddress);
    }

    private void SetId(DriverState driverState)
    {
        DriverState oldState = Get(driverState.IOAddress).Result;
        if (oldState == null)
            throw new ObjectNotFoundException($"Driver state with address {driverState.IOAddress} doesn't exist");
        driverState.Id = oldState.Id;
    }

    public async Task<IEnumerable<DriverState>> Update(IEnumerable<DriverState> driverStates)
    {
        Console.WriteLine(driverStates.Count());
        var bulkOps = new List<WriteModel<DriverState>>();
        driverStates.Where(x => x.IOAddress == 20).ToList().ForEach(x =>  Console.WriteLine("VALUE OF 20 IS: " +x.Value));
        bulkOps.AddRange(driverStates.Select(driverState =>
        {
            SetId(driverState);
            var filter = Builders<DriverState>.Filter.Eq(x => x.IOAddress, driverState.IOAddress);
            var update = Builders<DriverState>.Update .Set(x => x.Value, driverState.Value);

            return new UpdateOneModel<DriverState>(filter, update);
        }));

        if (bulkOps.Count > 0)
        {
            var options = new BulkWriteOptions { IsOrdered = false }; 
            await _states.BulkWriteAsync(bulkOps, options);
        }

        return driverStates;
    }
}