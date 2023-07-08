using System;
using Newtonsoft.Json.Linq;

namespace scada_core.TagProcessing
{
    public class TagProcessingService
    {
        private readonly TagProcessingClient _client;

        public TagProcessingService()
        {
            _client = new TagProcessingClient();
        }
        
        public JToken GetAllTags()
        {
            return _client.GetAllTags();
        }
        
        public   JToken  CreateTagRecord(string tagName, double tagValue)
        {
            JObject newRecord = new JObject(
                new JProperty("tagName", tagName),
                new JProperty("timestamp", DateTime.Now),
                new JProperty("tagValue", tagValue)
            );
            return _client.CreateTagRecord(newRecord);
        }
        
        public   JToken  GetDriverStateUrl(int ioAddress)
        {
            return _client.GetDriverState(ioAddress);
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
    }
}