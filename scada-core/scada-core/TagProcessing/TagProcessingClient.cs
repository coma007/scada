

using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using scada_core_6.ApiClient;

namespace scada_core.TagProcessing
{
    public class TagProcessingClient
    {
        private readonly ApiClient _apiClient;
        private string _getDriverStateUrl;
        private string _getAllTagsUrl;
        private string _createTagRecordUrl;

        public TagProcessingClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
            ConfigureUrls();
        }

        private void ConfigureUrls()
        {
            string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];;
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
    }
}