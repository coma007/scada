

using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using scada_core_6.ApiClient;

namespace scada_core.TagProcessing
{
    public class TagProcessingClient
    {
        private readonly ApiClient _apiClient;
        private string _createDriverStateUrl;
        private string _updateDriverStateUrl;
        private string _getDriverStateUrl;
        private string _getAllTagsUrl;
        private string _createTagRecordUrl;

        public TagProcessingClient()
        {
            _apiClient = new ApiClient();
            ConfigureUrls();
        }

        private void ConfigureUrls()
        {
            string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];;
            _createDriverStateUrl = apiUrl +  ConfigurationManager.AppSettings["CreateDriverStateUrl"];
            _updateDriverStateUrl = apiUrl +  ConfigurationManager.AppSettings["UpdateDriverStateUrl"];
            _getDriverStateUrl = apiUrl +  ConfigurationManager.AppSettings["GetDriverStateUrl"];
            _getAllTagsUrl = apiUrl +  ConfigurationManager.AppSettings["GetAllTagsByTypeUrl"];
            _createTagRecordUrl = apiUrl +  ConfigurationManager.AppSettings["CreateTagRecordUrl"];
        }

        public   JToken  GetAllTags(string tagType)
        {
            return _apiClient.MakeApiRequest(_getAllTagsUrl+tagType).Result;
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