using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public interface ITagService
{
    IEnumerable<TagDTO> GetAll();
    IEnumerable<TagDTO> GetAll(string discriminator);
    TagDTO Get(string tagName);
    TagDTO Create(TagDTO tag);
    TagDTO Delete(string tagName);
}