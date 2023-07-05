using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using scada_back.Database;
using scada_back.Tag.Model;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public class TagRepository : ITagRepository
{
    private IMongoCollection<Model.Abstraction.Tag> _tags;

    public TagRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        // BsonClassMap.RegisterClassMap<AbstractTag>(cm =>
        // {
        //     cm.AutoMap();
        //     cm.SetDiscriminator("analog_input");
        //     cm.SetDiscriminator("analog_output");
        //     cm.SetDiscriminator("digital_input");
        //     cm.SetDiscriminator("digital_output");
        // });
        
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _tags = _database.GetCollection<Model.Abstraction.Tag>(settings.TagsCollectionName);

    }
    
    public async Task<IEnumerable<Model.Abstraction.Tag>> GetAll()
    {
        return (await _tags.FindAsync(tag => true)).ToList();
    }

    public async Task<IEnumerable<Model.Abstraction.Tag>> GetAll(string discriminator)
    {
        var filter = Builders<Model.Abstraction.Tag>.Filter.Eq("_t", discriminator);
        return (await _tags.FindAsync<Model.Abstraction.Tag>(filter)).ToList();
    }

    public async Task<Model.Abstraction.Tag> Get(string tagName)
    {
        return (await _tags.FindAsync(tag => tag.TagName == tagName)).FirstOrDefault();
    }

    public async Task<Model.Abstraction.Tag> Create(Model.Abstraction.Tag tag)
    {
        await _tags.InsertOneAsync(tag);
        return await Get(tag.TagName);
    }

    public async Task<Model.Abstraction.Tag> Delete(string tagName)
    {
        Model.Abstraction.Tag tag = await Get(tagName);
        await _tags.DeleteOneAsync(tag => tag.TagName == tagName);
        return tag;
    }
}