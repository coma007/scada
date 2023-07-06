using MongoDB.Driver;
using scada_back.Database;
using scada_back.Exception;

namespace scada_back.Tag;

public class TagRepository : ITagRepository
{
    private IMongoCollection<Model.Abstraction.Tag> _tags;

    public TagRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _tags = database.GetCollection<Model.Abstraction.Tag>(settings.TagsCollectionName);

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

    public async Task<Model.Abstraction.Tag> Create(Model.Abstraction.Tag newTag)
    {
        await _tags.InsertOneAsync(newTag);
        return await Get(newTag.TagName);
    }

    public async Task<Model.Abstraction.Tag> Delete(string tagName)
    {
        Model.Abstraction.Tag toBeDeleted = await Get(tagName);
        DeleteResult result = await _tags.DeleteOneAsync(tag => tag.TagName == tagName);
        if (result.DeletedCount == 0)
        {
            throw new ActionNotExecutedException("Delete failed.");
        }
        return toBeDeleted;
    }

    public async Task<Model.Abstraction.Tag> Update(Model.Abstraction.Tag updatedTag)
    {
        ReplaceOneResult result = await _tags.ReplaceOneAsync(tag => tag.TagName == updatedTag.TagName, updatedTag);
        if (result.ModifiedCount == 0)
        {
            throw new ActionNotExecutedException("Update failed.");
        }

        return await Get(updatedTag.TagName);
    }
}