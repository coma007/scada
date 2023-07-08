using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.WebSocket;

public interface IWebSocketServer
{
    void NotifyClientAboutNewRecord(TagHistoryRecordDto record);
    void NotifyProcessingAppAboutNewTag(TagDto tag);
}