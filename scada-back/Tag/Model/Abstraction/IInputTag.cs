namespace scada_back.Tag.Model.Abstraction;

public interface IInputTag : IAbstractTag
{
    public object Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}

public interface IInputTagDTO : IAbstractTagDTO
{
    public object Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}