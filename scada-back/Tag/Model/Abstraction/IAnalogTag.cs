namespace scada_back.Tag.Model.Abstraction;

public interface IAnalogTag : IAbstractTag
{
    public double LowLimit { get; set; }
    public double HightLimit { get; set; }
    public string Units { get; set; }

}

public interface IAnalogTagDTO : IAbstractTagDTO
{
    public double LowLimit { get; set; }
    public double HightLimit { get; set; }
    public string Units { get; set; }

}