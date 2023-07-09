namespace scada_core.SimulationDriver;

public class RTU
{
    private double min;
    private double max;
    private int ioAddress;

    public RTU(double min, double max, int ioAddress)
    {
        this.min = min;
        this.max = max;
        this.ioAddress = ioAddress;
    }

    public double Min
    {
        get => min;
        set => min = value;
    }

    public double Max
    {
        get => max;
        set => max = value;
    }

    public int IoAddress
    {
        get => ioAddress;
        set => ioAddress = value;
    }
}