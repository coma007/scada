namespace scada_back.Infrastructure.Feature.AlarmHistory;

public interface IAlarmHistoryLogger
{
    public string FileName { get; set; }
    public void LogToFile(AlarmHistoryRecord record);
}