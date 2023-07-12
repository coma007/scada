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

    private const string _logTag = "BATCH MEDIATOR: ";
    public BatchMediator(ApiClient.ApiClient apiClient)
    {
        _service = new DriverService(apiClient);
        _states = new ConcurrentDictionary<int, double>();
    }

    public void notify(Driver sender, int ioAddress, double value)
    {
        if (!_pauseEvent.IsSet)
        {
            Console.WriteLine(_logTag + $"Address {ioAddress} Waits");
        }
        _pauseEvent.Wait();
        if (_pauseEvent.IsSet)
        {
            Console.WriteLine(_logTag + $"Update value, {ioAddress}: {value}");
            _states.AddOrUpdate(ioAddress, value,(k, oldValue) => value);
            
            IncrementCounter();
            lock (consoleLock)
            {
                Console.WriteLine(_logTag + $"Counter: {counter}");
            }

            if (Interlocked.CompareExchange(ref counter, 0, Int32.MaxValue) > limit)
            {
                // pause adding new elements
                _pauseEvent.Reset();
                Console.WriteLine(_logTag + "Pause Started");

                // wait for emptying structure before writing to it
                var toSave = Array.Empty<KeyValuePair<int, double>>();
                lock (_states)
                {
                    toSave = _states.ToArray();
                    _states.Clear();
                }

                try
                {
                    JToken token = _service.UpdateDriverStates(toSave);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(_logTag + $"ERROR {e.Message}");
                }

                ResetCounter();
                // Thread.Sleep(3000);

                // resume adding new elements
                Console.WriteLine(_logTag + "Pause Ended");
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