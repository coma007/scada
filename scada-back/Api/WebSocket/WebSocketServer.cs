using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging.Abstractions;
using scada_back.Infrastructure.Feature.AlarmHistory;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.WebSocket;

public class WebSocketServer : IWebSocketServer
{
    private readonly WebSocketHandler _handler;

    public WebSocketServer(WebSocketHandler handler)
    {
        _handler = handler;
    }
    public async void NotifyClientAboutNewTagRecord(TagHistoryRecordDto record)
    {
        await _handler.SendMessage("NewTagRecordCreated", record);
    }
    
    public async void NotifyClientAboutNewAlarmRecord(IEnumerable<AlarmHistoryRecordDto> record)
    {
        await _handler.SendMessage("NewAlarmRecordsCreated", record);
    }

    public async void NotifyClientAboutNewTag(TagDto tag)
    {
        await _handler.SendMessage("NewTagCreated", tag);
    }
    
    public async void NotifyClientAboutTagScan(TagDto tag)
    {
        await _handler.SendMessage("TagScanUpdated", tag);
    }

    public async void NotifyClientAboutTagDelete(TagDto tag)
    {
        await _handler.SendMessage("TagDeleted", tag);
    }

}