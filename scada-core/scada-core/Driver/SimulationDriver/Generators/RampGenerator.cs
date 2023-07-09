namespace scada_core.SimulationDriver.Signals;

public class RampGenerator : SignalGenerator
{
    public RampGenerator(int ioAddress)
    {
        this.ioAddress = base.ioAddress;
    }

    protected override double Get()
    {
        return t >= 0 ? t : 0;
    }
}