using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging.Abstractions;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.WebSocket;

public class WebSocketServer : Hub, IWebSocketServer
{
    private readonly IHubContext<WebSocketServer> _context;

    public WebSocketServer(IHubContext<WebSocketServer> context)
    {
        _context = context;
    }
    public async void NotifyClientAboutNewRecord(TagHistoryRecordDto record)
    {
        await Clients.All.SendAsync("NewRecordAdded", record);
    }

    public async void NotifyProcessingAppAboutNewTag(TagDto tag)
    {
            await Clients.Caller.SendAsync("NewTagCreated", tag);
    }
}