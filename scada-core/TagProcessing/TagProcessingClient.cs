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

        public TagProcessingClient(ApiClient.ApiClient apiClient)
        {
            _apiClient = apiClient;
            ConfigureUrls();
        }

        private void ConfigureUrls()
        {
            string apiUrl = ConfigurationSettings.AppSettings["ApiUrl"];
            _createDriverStateUrl = apiUrl + ConfigurationSettings.AppSettings["CreateDriverStateUrl"];
            _updateDriverStateUrl = apiUrl + ConfigurationSettings.AppSettings["UpdateDriverStateUrl"];
            _getDriverStateUrl = apiUrl + ConfigurationSettings.AppSettings["GetDriverStateUrl"];
            _getAllTagsUrl = apiUrl + ConfigurationSettings.AppSettings["GetAllTagsByTypeUrl"];
            _createTagRecordUrl = apiUrl + ConfigurationSettings.AppSettings["CreateTagRecordUrl"];
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
    }
}