using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using scada_core.ApiClient;
using scada_core.TagProcessing;

namespace scada_core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            TagProcessor tagProcessingService = new TagProcessor();
            
            tagProcessingService.InitializeTagThreads();
            
            WebSocketClient client = new WebSocketClient();
            await client.Connect();

        }
    }
}