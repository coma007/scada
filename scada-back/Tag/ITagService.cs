using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public interface ITagService
{
    IEnumerable<TagDto> GetAll();
    IEnumerable<TagDto> GetAll(string discriminator);
    TagDto Get(string tagName);
    TagDto Create(TagDto newTag);
    TagDto Delete(string tagName);
    TagDto Update(TagDto updatedTag);
}