using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using scada_core.ApiClient;
using scada_core.TagProcessing;

namespace scada_core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TagProcessor tagProcessingService = new TagProcessor();
            WebSocketClient webSocketClient = new WebSocketClient();
            tagProcessingService.InitializeTagThreads();
        }
    }
}