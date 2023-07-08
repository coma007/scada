using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace scada_core.TagProcessing
{
    public class TagProcessingClient
    {
        private readonly ApiClient.ApiClient _apiClient;
        private string _createDriverStateUrl;
        private string _updateDriverStateUrl;
        private string _getDriverStateUrl;
        private string _getAllTagsUrl;
        private string _createTagRecordUrl;

        public TagProcessingClient()
        {
            _apiClient = new ApiClient.ApiClient();
            ConfigureUrls();
        }

        private void ConfigureUrls()
        {
            NameValueCollection apiValues = ConfigurationSettings.GetConfig("apiEndpoints") as NameValueCollection;
            string apiUrl = apiValues["ApiUrl"];
            _createDriverStateUrl = apiUrl + apiValues["CreateDriverStateUrl"];
            _updateDriverStateUrl = apiUrl + apiValues["UpdateDriverStateUrl"];
            _getDriverStateUrl = apiUrl + apiValues["GetDriverStateUrl"];
            _getAllTagsUrl = apiUrl + apiValues["GetAllTagsUrl"];
            _createTagRecordUrl = apiUrl + apiValues["CreateTagRecordUrl"];
        }

        public   JToken  GetAllTags()
        {
            return _apiClient.MakeApiRequest(_getAllTagsUrl).Result;
        }
        
        public   JToken  CreateTagRecord(JObject newRecord)
        {
            HttpContent requestBody = new StringContent(newRecord.ToString(), Encoding.UTF8, "application/json");
            return _apiClient.MakeApiRequest(_createTagRecordUrl, HttpMethod.Post, requestBody).Result;
        }
        
        public   JToken  GetDriverState(int ioAddress)
        {
            return _apiClient.MakeApiRequest(_getDriverStateUrl+ioAddress).Result;
        }
        
        public  JToken CreateDriverState(JObject newState)
        {
            HttpContent requestBody = new StringContent(newState.ToString(), Encoding.UTF8, "application/json");
            return _apiClient.MakeApiRequest(_createDriverStateUrl, HttpMethod.Post, requestBody).Result;
        }
        
        public   JToken UpdateDriverState(JObject updatedState)
        {
            HttpContent requestBody = new StringContent(updatedState.ToString(), Encoding.UTF8, "application/json");
            return _apiClient.MakeApiRequest(_updateDriverStateUrl, new HttpMethod("PATCH"), requestBody).Result;
        }
    }
}