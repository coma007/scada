using Microsoft.AspNetCore.SignalR;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.WebSocket;

public class WebSocketServer : Hub, IWebSocketServer
{
    public void NotifyClientAboutNewRecord(TagHistoryRecordDto record)
    {
        Clients.All.SendAsync("NewRecordAdded", record);
    }

    public void NotifyProcessingAppAboutNewTag(TagDto tag)
    {
        Clients.All.SendAsync("NewTagCreated", tag);
    }
}