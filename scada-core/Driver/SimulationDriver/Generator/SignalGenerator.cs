namespace scada_core.Driver.SimulationDriver.Generator;

public abstract class SignalGenerator
{
    protected double t = 0;
    protected double stepSize = 0.7;
    protected int ioAddress;
    protected abstract double Get();

    public void NextStep()
    {
        t += stepSize;
    }

    public double GetAndNext()
    {
        double val = Get();
        NextStep();
        return val;
    }

    public int IoAddress
    {
        get => ioAddress;
        set => ioAddress = value;
    }
}