using System.Dynamic;

namespace scada_back.Alarm;

public interface IAlarmService
{
    public AlarmDTO Get(string id);
    public IEnumerable<AlarmDTO> GetAll();
    public AlarmDTO Create(AlarmCreateUpdateDTO updateDto);
    public AlarmDTO Delete(string id);
    public AlarmDTO Update(AlarmCreateUpdateDTO dto, string id);
}