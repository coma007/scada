using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace scada_core.TagProcessing
{
    public class TagProcessingService
    {
        private readonly TagProcessingClient _client;

        public TagProcessingService(ApiClient.ApiClient apiClient)
        {
            _client = new TagProcessingClient(apiClient);
        }
        
        public Dictionary<string, Dictionary<string, object>> GetAllTags(string tagType)
        {
            return ExtractTagsProperties(_client.GetAllTags(tagType));
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
        
        private Dictionary<string, Dictionary<string, object>> ExtractTagsProperties(JToken analogTags)
        {
            Dictionary<string, Dictionary<string, object>> tagProperties = new Dictionary<string, Dictionary<string, object>>();

            foreach (var tag in analogTags)
            {
                string? tagName = tag["tagName"]?.ToString();
                string? tagType = tag["tagType"]?.ToString();
                int? ioAddress = tag["ioAddress"]?.ToObject<int>();
                bool? scan = tag["scan"]?.ToObject<bool>();
                int? scanTime = tag["scanTime"]?.ToObject<int>();
                Dictionary<string, object> tagData = new Dictionary<string, object>
                {
                    { "ioAddress", ioAddress },
                    { "scan", scan },
                    { "scanTime", scanTime }
                };
                if (tagType != null && tagType.Contains("analog"))
                {
                    double? lowLimit = tag["lowLimit"]?.ToObject<double>();
                    double? highLimit = tag["highLimit"]?.ToObject<double>();
                    tagData.Add("lowLimit", lowLimit);
                    tagData.Add("highLimit", highLimit);
                }

                tagProperties.Add(tagName, tagData);
            }

            return tagProperties;
        }

        public static string? ExtractProperties(JToken tag, out Dictionary<string, object> tagData)
        {
            string? tagName = tag["TagName"]?.ToString();
            string? tagType = tag["TagType"]?.ToString();
            int? ioAddress = tag["IOAddress"]?.ToObject<int>();
            bool? scan = tag["Scan"]?.ToObject<bool>();
            int? scanTime = tag["ScanTime"]?.ToObject<int>();
            tagData = new Dictionary<string, object>
            {
                { "ioAddress", ioAddress },
                { "scan", scan },
                { "scanTime", scanTime }
            };
            if (tagType != null && tagType.Contains("analog"))
            {
                double? lowLimit = tag["LowLimit"]?.ToObject<double>();
                double? highLimit = tag["HighLimit"]?.ToObject<double>();
                tagData.Add("lowLimit", lowLimit);
                tagData.Add("highLimit", highLimit);
            }

            return tagName;
        }
    }
}