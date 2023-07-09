namespace scada_core.SimulationDriver.Signals;

public class TrigGenerator : SignalGenerator
{
    private double amplitude;
    private double frequency;
    private double phase;

    public delegate double TrigFunction(double t);

    private TrigFunction func;

    public TrigGenerator(TrigFunction func, double ioAddress)
    {
        amplitude = 1;
        frequency = 1 / (2f * Math.PI);
        phase = 0;
        
        this.func = func;
    }

    public TrigGenerator(double amplitude, double frequency, double phase, TrigFunction func, int ioAddress)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.phase = phase;
        
        this.func = func;
        this.ioAddress = ioAddress;
    }

    protected override double Get()
    {
        double shifted = amplitude * (frequency * t + phase);
        return func(2f * Math.PI * shifted);
    }
}