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
        
        _generators.Add(new TrigGenerator(Math.Sin, 0));
        _generators.Add(new TrigGenerator(Math.Sin, 1));
        _generators.Add(new TrigGenerator(Math.Sin, 2));
        
        _generators.Add(new TrigGenerator(Math.Cos, 3));
        _generators.Add(new TrigGenerator(Math.Cos, 4));
        _generators.Add(new TrigGenerator(Math.Cos, 5));
        
        _generators.Add(new RampGenerator(6));
        _generators.Add(new RampGenerator(7));
        _generators.Add(new RampGenerator(8));
        _generators.Add(new RampGenerator(9));

        // for (int i = 0; i < _generators.Count; i++)
        // {
        //     JToken token = _service.CreateDriverState(i, 0);
        // }
    }

    public void Simulate()
    {
        for (int i = 0; i < _generators.Count; i++)
        {
            SignalGenerator sg = _generators[i];
            _tasks.Add(Task.Run(() => Generate(sg)));
        }

        Task.WaitAll(_tasks.ToArray());
    }

    private async Task Generate(SignalGenerator generator)
    {
        var gen = generator;
        while (true)
        {
            double value = gen.GetAndNext();
            try
            {
                JToken token = _service.UpdateDriverState(gen.IoAddress, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalGenerator: An error occurred while sending the random number: {ex.Message}");
            }

            Thread.Sleep(1000);
        }
    }
}