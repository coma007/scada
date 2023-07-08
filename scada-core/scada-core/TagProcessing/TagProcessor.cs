using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using scada_core_6.ApiClient;

namespace scada_core.TagProcessing
{
    public class TagProcessor
    {
        private readonly TagProcessingService _service;
        private Dictionary<string, Dictionary<string, object>> _tagProperties;
        private Dictionary<string, Thread> _tagThreads;

        public TagProcessor(ApiClient apiClient)
        {
            _service = new TagProcessingService(apiClient);
            _tagProperties = new Dictionary<string, Dictionary<string, object>>();
            _tagThreads = new Dictionary<string, Thread>();
        }

        public void InitializeTagThreads()
        {
            GetAllTags();
            CreateThreads();
        }

        private void CreateThreads()
        {
            foreach (var tag in _tagProperties)
            {
                string tagName = tag.Key;
                var tagAttributes = tag.Value;
                bool scan = (bool)tagAttributes["scan"];
                int scanTime = (int)tagAttributes["scanTime"];
                
                if (scan)
                {
                    Thread tagThread = new Thread(() =>
                    {
                        while (true)
                        {
                            Thread.Sleep(scanTime * 1000);
                            ProcessTag(tag);
                        }
                    });
                    tagThread.Start();
                    _tagThreads[tagName] = tagThread;
                }
            }
        }

        private void ProcessTag(KeyValuePair<string, Dictionary<string, object>> tag)
        {
            string tagName = tag.Key;
            var tagAttributes = tag.Value;
            double value = _service.GetDriverState((int)tagAttributes["ioAddress"])["value"]!.ToObject<double>();
            
            double lowLimit = tagAttributes.ContainsKey("lowLimit") ? (double)tagAttributes["lowLimit"] : double.MinValue;
            double highLimit = tagAttributes.ContainsKey("highLimit") ? (double)tagAttributes["highLimit"] : double.MaxValue;

            if (value <= lowLimit && value >= highLimit)
            {
                return;
            }
            Console.WriteLine("Logging tag value - tagName: " + tagName + "\t value: " + value);
            _service.CreateTagRecord(tagName, value);
        }

        private void GetAllTags()
        {
            _tagProperties = _tagProperties.Concat(_service.GetAllTags("analog_input")).ToDictionary(tag => tag.Key, tag => tag.Value);
            _tagProperties = _tagProperties.Concat(_service.GetAllTags("digital_input")).ToDictionary(tag => tag.Key, tag => tag.Value);
        }
    }
}