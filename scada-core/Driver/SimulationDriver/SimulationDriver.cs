using Newtonsoft.Json.Linq;
using scada_core.Driver.SimulationDriver.Generator;

namespace scada_core.Driver.SimulationDriver;

public class SimulationDriver : Driver
{
    private readonly IBatchMediator _batchMediator;
    
    private List<SignalGenerator> _generators;
    private List<Task> _tasks;
    private readonly DriverService _service;

    public SimulationDriver(IBatchMediator batchMediator, DriverService service)
    {
        _service = service;
        _batchMediator = batchMediator;
        
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

        // create addresses
        for (int i = 0; i < _generators.Count; i++)
        {
            try
            {
                // JToken token = _service.CreateDriverState(i, 0);
            }
            catch (System.Exception e)
            {
                
            }
        }
    }

    public override void Simulate()
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
                _batchMediator.notify(this, gen.IoAddress, value);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"SignalGenerator: An error occurred while sending the random number: {ex.Message}");
            }

            Thread.Sleep(waitTime);
        }
    }
}