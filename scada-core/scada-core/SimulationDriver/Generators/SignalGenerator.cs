namespace scada_core.SimulationDriver.Signals;

public abstract class SignalGenerator
{
    protected double t = 0;
    protected double stepSize = 1 / 1000;
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
}