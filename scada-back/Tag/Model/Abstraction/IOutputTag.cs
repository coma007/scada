
namespace scada_back.Tag.Model.Abstraction;

public interface IOutputTag : IAbstractTag
{
    public double InitialValue { get; set; }

}

public interface IOutputTagDTO : IAbstractTagDTO
{
    public double InitialValue { get; set; }

}