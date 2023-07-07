
namespace scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

public interface IOutputTag 
{
    public double InitialValue { get; set; }

}

public interface IOutputTagDto
{
    public double InitialValue { get; set; }

}