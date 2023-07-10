using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;

namespace scada_core.Driver;

public class BatchMediator : IBatchMediator
{
    private ConcurrentDictionary<int, double> _states;
    private readonly DriverService _service;
    private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
    private static int counter = 0;
    private static readonly object consoleLock = new object();
    private static int limit = 15;
    public BatchMediator(ApiClient.ApiClient apiClient)
    {
        _service = new DriverService(apiClient);
        _states = new ConcurrentDictionary<int, double>();
    }

    public void notify(Driver sender, int ioAddress, double value)
    {
        if(ioAddress == 20)
            Console.WriteLine($"20 is in batch, NEW 20 value: {value} after batch end");
        if (!_pauseEvent.IsSet)
        {
            Console.WriteLine($"{ioAddress} WAITS");
        }
        _pauseEvent.Wait();
        if (_pauseEvent.IsSet)
        {
            Console.WriteLine($"UPDATE VALUE, {ioAddress}: {value}");
            _states.AddOrUpdate(ioAddress, value,
                // Update factory delegate for updating an existing key-value pair
                (k, oldValue) => value);
            IncrementCounter();
            lock (consoleLock)
            {
                Console.WriteLine(counter);
            }

            if (Interlocked.CompareExchange(ref counter, 0, Int32.MaxValue) > limit)
            {
                Console.WriteLine("IN CONDITION");
                // pause adding new elements
                _pauseEvent.Reset();
                Console.WriteLine("PAUSE STARTED");

                var toSave = _states.ToArray();
                _states.Clear();
                ResetCounter();
                JToken token = _service.UpdateDriverStates(toSave);
                // Thread.Sleep(3000);

                Console.WriteLine("PAUSE ENDED");
                // resume adding new elements
                _pauseEvent.Set();
            }
        }
    }
    static void IncrementCounter()
    {
        Interlocked.Increment(ref counter);
    }
    
    static void ResetCounter()
    {
        Interlocked.Exchange(ref counter, 0);
    }
}