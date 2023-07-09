using Newtonsoft.Json.Linq;
using scada_core_6.ApiClient;
using scada_core.Driver;
using scada_core.SimulationDriver.Signals;

namespace scada_core.SimulationDriver;

public class SimulationDriver
{
    private List<SignalGenerator> _generators;
    private List<Task> _tasks;
    private readonly DriverService _service;

    public SimulationDriver(ApiClient apiClient)
    {
        _service = new DriverService(apiClient);
        
        _generators = new List<SignalGenerator>();
        _tasks = new List<Task>();
        
        _generators.Add(new TrigGenerator(Math.Sin, 1));
        _generators.Add(new TrigGenerator(Math.Sin, 2));
        _generators.Add(new TrigGenerator(Math.Sin, 3));
        
        _generators.Add(new TrigGenerator(Math.Cos, 4));
        _generators.Add(new TrigGenerator(Math.Cos, 5));
        _generators.Add(new TrigGenerator(Math.Cos, 6));
        
        _generators.Add(new RampGenerator(7));
        _generators.Add(new RampGenerator(8));
        _generators.Add(new RampGenerator(9));
    }

    public void Simulate()
    {
        for (int i = 0; i < _generators.Count; i++)
        {
            SignalGenerator sg = _generators[i];
            _tasks.Add(Task.Run(() => Generate(sg)));
        }

        var arr = _tasks.ToArray();
        Task.WaitAll(arr);
    }

    private async Task Generate(SignalGenerator generator)
    {
        while (true)
        {
            double value = generator.GetAndNext();
            try
            {
                JToken token = _service.UpdateDriverState(generator.IoAddress, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the random number: {ex.Message}");
            }

            Thread.Sleep(1000);
        }
    }
}