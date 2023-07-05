using scada_back.Tag;

namespace scada_back.TagHistory;

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
        IEnumerable<TagHistoryRecord> tags =  _repository.GetAll().Result;
        return tags.Select(tag => tag.ToDto());

    }

    public IEnumerable<TagHistoryRecordDto> GetAll(string tagName)
    {
        IEnumerable<TagHistoryRecord> tags =  _repository.GetAll(tagName).Result;
        return tags.Select(tag => tag.ToDto());
    }

    public IEnumerable<TagHistoryRecordDto> GetBetween(DateTime startDateTime, DateTime endDateTime)
    {
        IEnumerable<TagHistoryRecord> tags =  _repository.GetBetween(startDateTime, endDateTime).Result;
        return tags.Select(tag => tag.ToDto());
    }

    public void Create(TagHistoryRecordDto newRecord)
    {
        _tagService.Get(newRecord.TagName);
        _repository.Create(newRecord.ToEntity());
    }
}