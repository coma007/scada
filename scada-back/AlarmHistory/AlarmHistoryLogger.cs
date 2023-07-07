using scada_back.Exception;

namespace scada_back.AlarmHistory;

public class AlarmHistoryLogger : IAlarmHistoryLogger
{
    public string FileName { get; set; }

    public AlarmHistoryLogger(IConfiguration configuration)
    {
        FileName = configuration.GetSection("AlarmHistoryLogger:FileName").Value;
    }
    
    public void LogToFile(AlarmHistoryRecord record)
    {
        string logText = $"{record.AlarmName}\t{record.Timestamp}\t{record.TagValue}";
        try
        {
            using StreamWriter writer = new StreamWriter(FileName, true);
            writer.WriteLine(logText);
        }
        catch (System.Exception ex)
        {
            throw new ActionNotExecutedException("File logging failed.");
        }
    }
}