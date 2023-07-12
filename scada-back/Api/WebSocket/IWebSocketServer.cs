using scada_back.Infrastructure.Feature.AlarmHistory;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.WebSocket;

public interface IWebSocketServer
{
    void NotifyClientAboutNewTagRecord(TagHistoryRecordDto record);
    void NotifyClientAboutNewAlarmRecord(IEnumerable<AlarmHistoryRecordWebSocketDto> records);
    void NotifyClientAboutNewTag(TagDto tag);
    void NotifyClientAboutTagScan(TagDto tag);
    void NotifyClientAboutTagDelete(TagDto tag);
}