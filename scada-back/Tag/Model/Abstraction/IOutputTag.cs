
namespace scada_back.Tag.Model.Abstraction;

public interface IOutputTag 
{
    public double InitialValue { get; set; }

}

public interface IOutputTagDTO
{
    public double InitialValue { get; set; }

}