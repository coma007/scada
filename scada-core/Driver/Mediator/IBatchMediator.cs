namespace scada_core.Driver;

public interface IBatchMediator
{
    public void notify(Driver sender, int ioAddress, double value);
}