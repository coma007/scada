
namespace scada_back.Infrastructure.Feature.DriverState;

public interface IDriverStateService
{
    IEnumerable<DriverStateDto> GetAll();
    DriverStateDto Get(int ioAddress);
    DriverStateDto Create(DriverStateDto driverState);
    DriverStateDto Update(DriverStateDto driverState);
}