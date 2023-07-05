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
    
    public IEnumerable<IAbstractTag> GetAll()
    {
        return _tags.Find(tag => true).ToList();
    }

    public IEnumerable<IAbstractTag> GetAllAnalogTags()
    {
        return _tags.Find(tag => tag.SignalType == "analog").ToList();
    }

    public IEnumerable<IAbstractTag> GetAllAnalogInputTags()
    {
        return _tags.Find(tag => tag.SignalType == "analog" && tag.TagType == "input").ToList();
    }

    public IEnumerable<IAbstractTag> GetAllAnalogOutputTags()
    {
        return _tags.Find(tag => tag.SignalType == "analog" && tag.TagType == "output").ToList();
    }

    public IEnumerable<IAbstractTag> GetAllDigitalTags()
    {
        return _tags.Find(tag => tag.SignalType == "digital").ToList();
    }

    public IEnumerable<IAbstractTag> GetAllDigitalInputTags()
    {
        return _tags.Find(tag => tag.SignalType == "digital" && tag.TagType == "input").ToList();
    }

    public IEnumerable<IAbstractTag> GetAllDigitalOutputTags()
    {
        return _tags.Find(tag => tag.SignalType == "digital" && tag.TagType == "output").ToList();
    }

    public IAbstractTag Get(string tagName)
    {
        return _tags.Find(tag => tag.TagName == tagName).FirstOrDefault();
    }

    public IAbstractTag Create(IAbstractTag tag)
    {
        _tags.InsertOne(tag);
        return Get(tag.Id);
    }

    public IAbstractTag Delete(string tagName)
    {
        IAbstractTag tag = Get(tagName);
        _tags.DeleteOne(tag => tag.TagName == tagName);
        return tag;
    }
}