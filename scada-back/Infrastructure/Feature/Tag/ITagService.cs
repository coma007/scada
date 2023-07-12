using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

namespace scada_back.Infrastructure.Feature.Tag;

public interface ITagService
{
    IEnumerable<TagDto> GetAll();
    IEnumerable<TagDto> GetAll(string discriminator);
    IEnumerable<string> GetAllNames(string signalType);
    IEnumerable<string> GetInputScanNames();
    TagDto Get(string tagName);
    TagDto Create(TagDto newTag);
    TagDto Delete(string tagName);
    TagDto UpdateScan(string tagName);
    TagDto UpdateOutputValue(string tagName, double value);

}