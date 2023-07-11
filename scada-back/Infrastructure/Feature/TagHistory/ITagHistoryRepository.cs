namespace scada_back.Infrastructure.Feature.TagHistory;

public interface ITagHistoryRepository
{
    Task<IEnumerable<TagHistoryRecord>> GetAll();
    Task<IEnumerable<TagHistoryRecord>> GetAll(string tagName);
    Task<IEnumerable<TagHistoryRecord>> GetBetween(DateTime startDateTime, DateTime endDateTime);
    Task<IEnumerable<TagHistoryRecord>> GetLast(IEnumerable<string> tagNames);
    void Create(TagHistoryRecord newRecord);
    Task<TagHistoryRecord> GetLastForTag(string tagName);
}