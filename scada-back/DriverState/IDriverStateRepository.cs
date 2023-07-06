using System.Collections;

namespace scada_back.DriverState;

public interface IDriverStateRepository
{
    Task<IEnumerable<DriverState>> GetAll();
    Task<DriverState> Get(int dtoIoAddress);
    Task<DriverState> Create(DriverState state);
    Task<DriverState> Update(DriverState toEntity);
}