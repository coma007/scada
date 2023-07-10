namespace scada_core.Driver;

public abstract class  Driver
{
    public int waitTime = 5000;
    public abstract void Simulate();
}