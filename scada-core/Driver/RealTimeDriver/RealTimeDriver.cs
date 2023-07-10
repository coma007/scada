using Newtonsoft.Json.Linq;
using scada_core.SimulationDriver;

namespace scada_core.Driver.RealTimeDriver;

public class RealTimeDriver : Driver
{
    private List<RTU> _rtus;
    private List<Task> _tasks;

    private readonly IBatchMediator _batchMediator;

    public RealTimeDriver(IBatchMediator batchMediator)
    {

        _batchMediator = batchMediator;
        
        _rtus = new List<RTU>();
        _tasks = new List<Task>();

        for (int i = 10; i < 21; i++)
        {
            _rtus.Add(new RTU(0, 10, i));
            // JToken token = _service.CreateDriverState(i, 0);
        }
    }

    public override void Simulate()
    {
        for (int i = 0; i < _rtus.Count; i++)
        {
            RTU rtu = _rtus[i];
            _tasks.Add(Task.Run(() => Generate(rtu)));
        }

        Task.WaitAll(_tasks.ToArray());
    }

    private async Task Generate(RTU rtu)
    {
        Random r = new Random();
        while (true)
        {
            double range = rtu.Max - rtu.Min;
            double value = r.NextDouble() * range + rtu.Min;
            try
            {
                _batchMediator.notify(this, rtu.IoAddress, value);
                // JToken token = _service.UpdateDriverState(rtu.IoAddress, value);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the random number: {ex.Message}");
            }

            Thread.Sleep(waitTime);
        }
    }
}