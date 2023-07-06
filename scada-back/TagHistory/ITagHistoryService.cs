namespace scada_back.TagHistory;

public interface ITagHistoryService
{
    IEnumerable<TagHistoryRecordDto> GetAll();
    IEnumerable<TagHistoryRecordDto> GetAll(string tagName);
    IEnumerable<TagHistoryRecordDto> GetBetween(DateTime startDateTime, DateTime endDateTime);
    IEnumerable<TagHistoryRecordDto> GetLast(string signalType);
    void Create(TagHistoryRecordDto newRecord);
}