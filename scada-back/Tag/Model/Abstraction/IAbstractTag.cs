using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Tag.Model.Abstraction;

public interface IAbstractTag
{
    public string Id { get; set; } 
    public string TagName { get; set; }
    public string TagType { get; set; }
    public string SignalType { get; set; } 
    public string Description { get; set; }
    public string IOAddress { get; set; }

    public IAbstractTagDTO ToDTO();
}

public interface IAbstractTagDTO
{
    public string TagName { get; set; }
    public string TagType { get; set; }
    public string SignalType { get; set; } 
    public string Description { get; set; }
    public string IOAddress { get; set; }
    
}