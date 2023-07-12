namespace scada_core.SimulationDriver;

public class RTU
{
    private double _min;
    private double _max;
    private int _ioAddress;

    public RTU(double min, double max, int ioAddress)
    {
        this._min = min;
        this._max = max;
        this._ioAddress = ioAddress;
    }

    public double Min
    {
        get => _min;
        set => _min = value;
    }

    public double Max
    {
        get => _max;
        set => _max = value;
    }

    public int IoAddress
    {
        get => _ioAddress;
        set => _ioAddress = value;
    }
}