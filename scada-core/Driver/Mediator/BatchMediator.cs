using System.Collections.Concurrent;

namespace scada_core.Driver;

public class BatchMediator : IBatchMediator
{
    private ConcurrentDictionary<int, double> _states;
    private readonly DriverService _service;
    private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
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
            if (_states.Count > 100)
            {
                // pause adding new elements
                _pauseEvent.Reset();
            
                var toSave = _states.ToArray();
                _states.Clear();
                _service.UpdateDriverStates(toSave);
            
                // resume adding new elements
                _pauseEvent.Set();
            }
        }
        

    }
}