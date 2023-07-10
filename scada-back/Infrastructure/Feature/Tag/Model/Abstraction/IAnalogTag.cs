namespace scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

public interface IAnalogTag
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; }

}

public interface IAnalogTagDto
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; }

}