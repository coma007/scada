using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public interface ITagRepository
{
    IEnumerable<IAbstractTag> GetAll();
    
    IAbstractTag Get(string id);
}