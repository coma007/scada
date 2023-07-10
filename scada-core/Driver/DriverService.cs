using Newtonsoft.Json.Linq;

namespace scada_core.Driver;

public class DriverService
{
    private readonly DriverClient _client;

    public DriverService(ApiClient.ApiClient apiClient)
    {
        _client = new DriverClient(apiClient);
    }
    
    public  JToken CreateDriverState(int ioAddress, double value)
    {
        JObject newState = new JObject(
            new JProperty("ioAddress", ioAddress),
            new JProperty("value", value)
        );
        return _client.CreateDriverState(newState);
    }
        
    public   JToken UpdateDriverState(int ioAddress, double value)
    {
        JObject updatedState = new JObject(
            new JProperty("ioAddress", ioAddress),
            new JProperty("value", value)
        );
        return _client.UpdateDriverState(updatedState);
    }

    public JToken UpdateDriverStates(KeyValuePair<int, double>[] toSave)
    {
        JObject json = new JObject();
        json["list"] = JToken.FromObject(toSave);
        return _client.UpdateDriverStates(json);
    }
}