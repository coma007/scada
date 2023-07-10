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
    private static int limit = 25;
    public BatchMediator(ApiClient.ApiClient apiClient)
    {
        _service = new DriverService(apiClient);
        _states = new ConcurrentDictionary<int, double>();
    }

    public void notify(Driver sender, int ioAddress, double value)
    {
        if (_pauseEvent.IsSet)
        {
            _states.TryAdd(ioAddress, value);
            IncrementCounter();
            // lock (consoleLock)
            // {
            //     Console.WriteLine(counter);
            // }
            
            if (Interlocked.CompareExchange(ref counter, 0, limit) > limit)
            {
                // pause adding new elements
                _pauseEvent.Reset();
                
                var toSave = _states.ToArray();
                _states.Clear();
                ResetCounter();
                JToken token =  _service.UpdateDriverStates(toSave);
            
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