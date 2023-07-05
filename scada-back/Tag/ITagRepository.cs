using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public interface ITagRepository
{
    Task<IEnumerable<IAbstractTag>> GetAll();
    Task<IEnumerable<IAbstractTag>> GetAllAnalogTags();
    Task<IEnumerable<IAbstractTag>> GetAllAnalogInputTags();
    Task<IEnumerable<IAbstractTag>> GetAllAnalogOutputTags();
    Task<IEnumerable<IAbstractTag>> GetAllDigitalTags();
    Task<IEnumerable<IAbstractTag>> GetAllDigitalInputTags();
    Task<IEnumerable<IAbstractTag>> GetAllDigitalOutputTags();
    Task<IAbstractTag> Get(string tagName);
    Task<IAbstractTag> Create(IAbstractTag tag);
    Task<IAbstractTag> Delete(string tagName);
    
}