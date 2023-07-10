using System.Collections.Concurrent;

namespace scada_core.Driver;

public class BatchMediator : IBatchMediator
{
    private ConcurrentDictionary<int, double> _states;
    private readonly DriverService _service;
    private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
    private static int counter = 0;
    public BatchMediator(ApiClient.ApiClient apiClient)
    {
        _service = new DriverService(apiClient);
        _states = new ConcurrentDictionary<int, double>();
    }

    public void notify(Driver sender, int ioAddress, double value)
    {
        // if adding is paused wait to add new values, implementation without skipping values
        // _pauseEvent.Wait(); 

        // if it is not paused add, else just skip new values
        if (_pauseEvent.IsSet)
        {
            _states.TryAdd(ioAddress, value);
            IncrementCounter();
            
            if (_states.Count > 100)
            {
                // pause adding new elements
                _pauseEvent.Reset();
            
                var toSave = _states.ToArray();
                _states.Clear();
                ResetCounter();
                _service.UpdateDriverStates(toSave);
            
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