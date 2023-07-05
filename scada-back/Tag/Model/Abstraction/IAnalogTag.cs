namespace scada_back.Tag.Model.Abstraction;

public interface IAnalogTag
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; }

}

public interface IAnalogTagDTO
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; }

}