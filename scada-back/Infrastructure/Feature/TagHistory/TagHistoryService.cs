using scada_back.Api.WebSocket;
using scada_back.Infrastructure.Feature.Alarm;
using scada_back.Infrastructure.Feature.AlarmHistory;
using scada_back.Infrastructure.Feature.Tag;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

namespace scada_back.Infrastructure.Feature.TagHistory;

public class TagHistoryService : ITagHistoryService
{
    private readonly ITagHistoryRepository _repository;
    private readonly ITagService _tagService;
    private readonly IAlarmHistoryService _alarmHistoryService;
    private readonly IWebSocketServer _webSocketServer;

    public TagHistoryService(ITagHistoryRepository repository, IAlarmHistoryService alarmHistoryService, ITagService tagService,
        IWebSocketServer webSocketServer)
    {
        _repository = repository;
        _tagService = tagService;
        _alarmHistoryService = alarmHistoryService;
        _webSocketServer = webSocketServer;
    }
    
    public IEnumerable<TagHistoryRecordDto> GetAll()
    {
        IEnumerable<TagHistoryRecord> records =  _repository.GetAll().Result;
        return records.Select(record => record.ToDto());

    }

    public IEnumerable<TagHistoryRecordDto> GetAll(string tagName)
    {
        IEnumerable<TagHistoryRecord> records =  _repository.GetAll(tagName).Result;
        return records.Select(record => record.ToDto());
    }

    public IEnumerable<TagHistoryRecordDto> GetBetween(DateTime startDateTime, DateTime endDateTime)
    {
        IEnumerable<TagHistoryRecord> records =  _repository.GetBetween(startDateTime, endDateTime).Result;
        return records.Select(record => record.ToDto());
    }

    public IEnumerable<TagHistoryRecordDto> GetLast(string signalType)
    {
        IEnumerable<string> tagNames = _tagService.GetAllNames(signalType).Result;
        IEnumerable<TagHistoryRecord> records  = _repository.GetLast(tagNames).Result;
        return records.Select(record => record.ToDto());
    }

    public void Create(TagHistoryRecordDto newRecord)
    {
        TagDto tag = _tagService.Get(newRecord.TagName);
        IEnumerable<AlarmHistoryRecordDto> alarms = _alarmHistoryService.AlarmIfNeeded(tag.TagName, newRecord.TagValue);
        newRecord.Timestamp = DateTime.Now;
        _repository.Create(newRecord.ToEntity());
        _webSocketServer.NotifyClientAboutNewTagRecord(newRecord);
        if (alarms.Any())
        {
            List<AlarmHistoryRecordWebSocketDto> webSocketAlarms = new List<AlarmHistoryRecordWebSocketDto>();
            foreach (AlarmHistoryRecordDto a in alarms)
            {
                webSocketAlarms.Add(a.ToWebSocketDto(newRecord.TagName));
            }
            _webSocketServer.NotifyClientAboutNewAlarmRecord(webSocketAlarms);
        }
    }

    public string GetLastValueForTag(string tagName)
    {
        TagHistoryRecord tagValue = _repository.GetLastForTag(tagName).Result;
        if (tagValue == null) return "NaN";
        return tagValue.TagValue.ToString();
    }
}