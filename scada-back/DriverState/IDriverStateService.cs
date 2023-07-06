using System.Collections;

namespace scada_back.DriverState;

public interface IDriverStateService
{
    IEnumerable<DriverStateDTO> GetAll();
    DriverStateDTO Create(DriverStateDTO dto);
    DriverStateDTO Update(DriverStateDTO dto);
    DriverStateDTO Get(int address);
}