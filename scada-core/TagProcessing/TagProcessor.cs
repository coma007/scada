using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace scada_core.TagProcessing
{
    public class TagProcessor
    {
        private readonly TagProcessingService _service;
        private Dictionary<string, Dictionary<string, object>> _tagProperties;
        private Dictionary<string, Thread> _tagThreads;
        
        private const string _logTag = "TAG PROCCESSING: ";

        public TagProcessor(ApiClient.ApiClient apiClient)
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

        public void AddTag(Dictionary<string, object> newTag)
        {
            _tagProperties.Add((string)newTag["tagName"], newTag);
            foreach (var tag in _tagProperties)
            {
                if (tag.Key.Equals(newTag["tagName"]))
                {
                    CreateThread(tag);
                    break;
                }
            }
        }

        public void RemoveTag(Dictionary<string, object> deletedTag)
        {
            foreach (var tag in _tagProperties)
            {
                if (tag.Key.Equals(deletedTag["tagName"]))
                {
                    RemoveThread(tag);
                    break;
                }
            }
        }

        public void ChangeScan(Dictionary<string, object> updatedTag)
        {
            foreach (var tag in _tagProperties)
            {
                if (tag.Key.Equals(updatedTag["tagName"]))
                {
                    bool scan = (bool)tag.Value["scan"];
                    tag.Value["scan"] = !scan;

                    if (!scan)
                    {
                        CreateThread(tag);
                    }
                    else
                    {
                        RemoveThread(tag);
                    }

                    break;
                }
            }
        }

        private void CreateThreads()
        {
            foreach (var tag in _tagProperties)
            {
                CreateThread(tag);
            }
        }

        private void CreateThread(KeyValuePair<string, Dictionary<string, object>> tag)
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
                        try
                        {
                            Thread.Sleep(scanTime * 1000);
                        }
                        catch (ThreadInterruptedException e)
                        {
                            break;
                        }

                        ProcessTag(tag);
                    }
                });
                tagThread.Start();
                _tagThreads[tagName] = tagThread;
            }
        }

        private void RemoveThread(KeyValuePair<string, Dictionary<string, object>> tag)
        {
            Thread t = _tagThreads[tag.Key];
            t.Interrupt();
            _tagThreads.Remove(tag.Key);
        }

        private void ProcessTag(KeyValuePair<string, Dictionary<string, object>> tag)
        {
            string tagName = tag.Key;
            var tagAttributes = tag.Value;
            Console.WriteLine(_logTag + $"Processed Tag: {tagName}");
        }

        private void GetAllTags()
        {
            _tagProperties = _tagProperties.Concat(_service.GetAllTags("analog_input")).ToDictionary(tag => tag.Key, tag => tag.Value);
            _tagProperties = _tagProperties.Concat(_service.GetAllTags("digital_input")).ToDictionary(tag => tag.Key, tag => tag.Value);
        }
    }
}