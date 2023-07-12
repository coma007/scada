namespace scada_back.Infrastructure.Feature.TagHistory;

public interface ITagHistoryRepository
{
    Task<IEnumerable<TagHistoryRecord>> GetAll();
    Task<IEnumerable<TagHistoryRecord>> GetAll(string tagName);
    Task<IEnumerable<TagHistoryRecord>> GetBetween(DateTime startDateTime, DateTime endDateTime);
    Task<IEnumerable<TagHistoryRecord>> GetLatest(IEnumerable<string> tagNames);
    Task<TagHistoryRecord> GetLastForTag(string tagName);
    Task<IEnumerable<TagHistoryRecord>> GetLastForTags(IEnumerable<string> tagNames);
    void Create(TagHistoryRecord newRecord);
}