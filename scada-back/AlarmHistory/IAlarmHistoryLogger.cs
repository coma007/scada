namespace scada_back.AlarmHistory;

public interface IAlarmHistoryLogger
{
    public string FileName { get; set; }
    public void LogToFile(AlarmHistoryRecord record);
}