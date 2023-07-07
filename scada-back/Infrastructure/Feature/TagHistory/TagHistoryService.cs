using scada_back.Infrastructure.Feature.Tag;

namespace scada_back.Infrastructure.Feature.TagHistory;

public class TagHistoryService : ITagHistoryService
{
    private readonly ITagHistoryRepository _repository;
    private readonly ITagService _tagService;

    public TagHistoryService(ITagHistoryRepository repository, ITagService tagService)
    {
        _repository = repository;
        _tagService = tagService;
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
        _tagService.Get(newRecord.TagName);
        _repository.Create(newRecord.ToEntity());
    }
}