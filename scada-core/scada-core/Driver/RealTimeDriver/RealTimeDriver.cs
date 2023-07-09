using Newtonsoft.Json.Linq;
using scada_core_6.ApiClient;
using scada_core.Driver;
using scada_core.SimulationDriver.Signals;

namespace scada_core.SimulationDriver;

public class RealTimeDriver
{
    private List<RTU> _rtus;
    private List<Task> _tasks;
    private readonly DriverService _service;

    public RealTimeDriver(ApiClient apiClient)
    {
        _service = new DriverService(apiClient);
        
        _rtus = new List<RTU>();
        _tasks = new List<Task>();
        
        for(int i = 11; i < 21; i++)
            _rtus.Add(new RTU(0, 10, i));
    }

    public void Simulate()
    {
        for (int i = 0; i < _rtus.Count(); i++)
        {
            _tasks.Add(Task.Run(() => Generate(_rtus[i])));
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
                JToken token = _service.UpdateDriverState(rtu.IoAddress, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the random number: {ex.Message}");
            }

            Thread.Sleep(1000);
        }
    }
}