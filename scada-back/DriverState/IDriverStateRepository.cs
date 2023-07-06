
namespace scada_back.DriverState;

public interface IDriverStateRepository
{
    Task<IEnumerable<DriverState>> GetAll();
    Task<DriverState> Get(int ioAddress);
    Task<DriverState> Create(DriverState driverState);
    Task<DriverState> Update(DriverState driverState);
}