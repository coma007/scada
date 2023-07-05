using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public interface ITagRepository
{
    IEnumerable<IAbstractTag> GetAll();
    IEnumerable<IAbstractTag> GetAllAnalogTags();
    IEnumerable<IAbstractTag> GetAllAnalogInputTags();
    IEnumerable<IAbstractTag> GetAllAnalogOutputTags();
    IEnumerable<IAbstractTag> GetAllDigitalTags();
    IEnumerable<IAbstractTag> GetAllDigitalInputTags();
    IEnumerable<IAbstractTag> GetAllDigitalOutputTags();
    IAbstractTag Get(string tagName);
    IAbstractTag Create(IAbstractTag tag);
    IAbstractTag Delete(string tagName);
    
}