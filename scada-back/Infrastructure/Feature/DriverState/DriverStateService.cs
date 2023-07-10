using scada_back.Infrastructure.Exception;

namespace scada_back.Infrastructure.Feature.DriverState;

public class DriverStateService : IDriverStateService
{
    
    private readonly IDriverStateRepository _repository;

    public DriverStateService(IDriverStateRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<DriverStateDto> GetAll()
    {
        IEnumerable<DriverState> alarms = _repository.GetAll().Result;
        return alarms.Select(alarm => alarm.ToDto());
    }
    
    public DriverStateDto Get(int ioAddress)
    {
        DriverState state = _repository.Get(ioAddress).Result;
        if (state == null)
        {
            throw new ObjectNotFoundException($"Driver with address '{ioAddress}' not found.");
        }
        return state.ToDto();
    }

    public DriverStateDto Create(DriverStateDto driverState)
    {
        DriverState existingDriverState = _repository.Get(driverState.IOAddress).Result;
        if (existingDriverState != null) {
            throw new ObjectNameTakenException($"Driver with address '{driverState.IOAddress}' already exists.");
        }
        DriverState state = driverState.ToEntity();
        return _repository.Create(state).Result.ToDto();
    }

    public DriverStateDto Update(DriverStateDto driverState)
    {
        return _repository.Update(driverState.ToEntity()).Result.ToDto();
    }
}