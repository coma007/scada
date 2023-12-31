using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;

namespace scada_core.Driver;

public class DriverClient
{
    
    private readonly ApiClient.ApiClient _apiClient;
    private string _createDriverStateUrl;
    private string _updateDriverStateUrl;
    private string _updateDriverStatesUrl;


    public DriverClient(ApiClient.ApiClient apiClient)
    {
        _apiClient = apiClient;
        ConfigureUrls();
    }

    private void ConfigureUrls()
    {
        string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];;
        _createDriverStateUrl = apiUrl +  ConfigurationManager.AppSettings["CreateDriverStateUrl"];
        _updateDriverStateUrl = apiUrl +  ConfigurationManager.AppSettings["UpdateDriverStateUrl"];
        _updateDriverStatesUrl = apiUrl +  ConfigurationManager.AppSettings["UpdateDriverStatesUrl"];
    }

    public  JToken CreateDriverState(JObject newState)
    {
        HttpContent requestBody = new StringContent(newState.ToString(), Encoding.UTF8, "application/json");
        return _apiClient.MakeApiRequest(_createDriverStateUrl, HttpMethod.Post, requestBody).Result;
    }
        
    public   JToken UpdateDriverState(JObject updatedState)
    {
        HttpContent requestBody = new StringContent(updatedState.ToString(), Encoding.UTF8, "application/json");
        var res = _apiClient.MakeApiRequest(_updateDriverStateUrl, new HttpMethod("PATCH"), requestBody).Result;
        return res;
    }

    public JToken UpdateDriverStates(JObject toUpdate)
    {
        HttpContent requestBody = new StringContent(toUpdate.ToString(), Encoding.UTF8, "application/json");
        var res = _apiClient.MakeApiRequest(_updateDriverStatesUrl, new HttpMethod("PATCH"), requestBody).Result;
        return res;
    }
}