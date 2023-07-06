
namespace scada_back.Tag.Model.Abstraction;

public interface IOutputTag 
{
    public double InitialValue { get; set; }

}

public interface IOutputTagDto
{
    public double InitialValue { get; set; }

}