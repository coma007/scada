namespace scada_core.SimulationDriver.Signals;

public class RampGenerator : SignalGenerator
{
    protected override double Get()
    {
        return t >= 0 ? t : 0;
    }
}