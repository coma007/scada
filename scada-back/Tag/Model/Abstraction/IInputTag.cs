using scada_back.Tag.Enumeration;

namespace scada_back.Tag.Model.Abstraction;

public interface IInputTag {
    public DriverType Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}

public interface IInputTagDto
{
    public string Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}