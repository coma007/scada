using scada_back.Exception;

namespace scada_back.DriverState;

public class DriverStateService : IDriverStateService
{
    
    private readonly IDriverStateRepository _repository;

    public DriverStateService(IDriverStateRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<DriverStateDTO> GetAll()
    {
        IEnumerable<DriverState> alarms = _repository.GetAll().Result;
        return alarms.Select(alarm => alarm.ToDto());
    }
    
    public DriverStateDTO Get(int address)
    {
        DriverState state = _repository.Get(address).Result;
        if (state == null)
        {
            throw new ObjectNotFoundException($"Driver with address '{address}' not found.");
        }
        return state.ToDto();
    }

    public DriverStateDTO Create(DriverStateDTO dto)
    {
        DriverState existingDriverState = _repository.Get(dto.IOAddress).Result;
        if (existingDriverState != null) {
            throw new ObjectNameTakenException($"Driver with address '{dto.IOAddress}' already exists.");
        }
        DriverState state = dto.ToEntity();
        return _repository.Create(state).Result.ToDto();
    }

    public DriverStateDTO Update(DriverStateDTO dto)
    {
        return _repository.Update(dto.ToEntity()).Result.ToDto();
    }
}