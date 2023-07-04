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

    public IAbstractTag Get(string id)
    {
        return _tags.Find(tag => tag.Id == id).FirstOrDefault();
    }
}