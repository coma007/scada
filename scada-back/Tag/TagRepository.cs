using MongoDB.Driver;
using scada_back.Database;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public class TagRepository : ITagRepository
{
    private IMongoCollection<IAbstractTag> _tags;

    public TagRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _tags = _database.GetCollection<IAbstractTag>(settings.TagsCollectionName);
    }
    
    public async Task<IEnumerable<IAbstractTag>> GetAll()
    {
        return (await _tags.FindAsync(tag => true)).ToList();
    }

    public async Task<IEnumerable<IAbstractTag>> GetAllAnalogTags()
    {
        return (await _tags.FindAsync(tag => tag.SignalType == "analog")).ToList();
    }

    public async Task<IEnumerable<IAbstractTag>> GetAllAnalogInputTags()
    {
        return (await _tags.FindAsync(tag => tag.SignalType == "analog" && tag.TagType == "input")).ToList();
    }

    public async Task<IEnumerable<IAbstractTag>> GetAllAnalogOutputTags()
    {
        return (await _tags.FindAsync(tag => tag.SignalType == "analog" && tag.TagType == "output")).ToList();
    }

    public async Task<IEnumerable<IAbstractTag>> GetAllDigitalTags()
    {
        return (await _tags.FindAsync(tag => tag.SignalType == "digital")).ToList();
    }

    public async Task<IEnumerable<IAbstractTag>> GetAllDigitalInputTags()
    {
        return (await _tags.FindAsync(tag => tag.SignalType == "digital" && tag.TagType == "input")).ToList();
    }

    public async Task<IEnumerable<IAbstractTag>> GetAllDigitalOutputTags()
    {
        return (await _tags.FindAsync(tag => tag.SignalType == "digital" && tag.TagType == "output")).ToList();
    }

    public async Task<IAbstractTag> Get(string tagName)
    {
        return (await _tags.FindAsync(tag => tag.TagName == tagName)).FirstOrDefault();
    }

    public async Task<IAbstractTag> Create(IAbstractTag tag)
    {
        await _tags.InsertOneAsync(tag);
        return await Get(tag.Id);
    }

    public async Task<IAbstractTag> Delete(string tagName)
    {
        IAbstractTag tag = await Get(tagName);
        await _tags.DeleteOneAsync(tag => tag.TagName == tagName);
        return tag;
    }
}