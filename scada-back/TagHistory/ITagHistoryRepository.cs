namespace scada_back.TagHistory;

public interface ITagHistoryRepository
{
    Task<IEnumerable<TagHistoryRecord>> GetAll();
    Task<IEnumerable<TagHistoryRecord>> GetAll(string tagName);
    Task<IEnumerable<TagHistoryRecord>> GetBetween(DateTime startDateTime, DateTime endDateTime);
    void Create(TagHistoryRecord newRecord);
}