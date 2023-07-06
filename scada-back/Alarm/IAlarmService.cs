using System.Dynamic;

namespace scada_back.Alarm;

public interface IAlarmService
{
    public AlarmDTO GetByName(string name);
    public IEnumerable<AlarmDTO> GetAll();
    public AlarmDTO Create(AlarmDTO updateDto);
    public AlarmDTO Delete(string name);
}