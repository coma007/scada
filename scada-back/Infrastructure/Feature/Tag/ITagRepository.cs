namespace scada_back.Infrastructure.Feature.Tag;

public interface ITagRepository
{
    Task<IEnumerable<Model.Abstraction.Tag>> GetAll();
    Task<IEnumerable<Model.Abstraction.Tag>> GetAll(string discriminator);
    Task<IEnumerable<string>> GetAllNames(string signalType);
    Task<Model.Abstraction.Tag> Get(string tagName);
    Task<Model.Abstraction.Tag> Create(Model.Abstraction.Tag newTag);
    Task<Model.Abstraction.Tag> Delete(string tagName);
    Task<Model.Abstraction.Tag> Update(Model.Abstraction.Tag updatedTag);
}