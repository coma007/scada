using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public interface ITagRepository
{
    Task<IEnumerable<Model.Abstraction.Tag>> GetAll();
    Task<IEnumerable<Model.Abstraction.Tag>> GetAll(string discriminator);
    Task<Model.Abstraction.Tag> Get(string tagName);
    Task<Model.Abstraction.Tag> Create(Model.Abstraction.Tag tag);
    Task<Model.Abstraction.Tag> Delete(string tagName);
    
}