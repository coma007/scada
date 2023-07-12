namespace scada_core.Driver.SimulationDriver.Generator;

public class DigitalGenerator : SignalGenerator
{
    private static Random _random = new Random();

    public DigitalGenerator(int ioAddress)
    {
        this.ioAddress = ioAddress;
    }

    protected override double Get()
    {
        return _random.Next(2);
    }
}