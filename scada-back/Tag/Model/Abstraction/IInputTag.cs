namespace scada_back.Tag.Model.Abstraction;

public interface IInputTag {
    public string Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}

public interface IInputTagDto
{
    public string Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}