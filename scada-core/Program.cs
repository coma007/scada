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
            // WebSocketClient webSocketClient = new WebSocketClient();
            
            var connection = new HubConnectionBuilder()
                .WithUrl("ws://localhost:7109/signalr-hub")
                .Build();
            
            connection.Closed += async (error) =>
            {
                Console.WriteLine("Connection closed. Reconnecting...");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            
            connection.On<string>("NewTagCreated", (message) =>
            {
                Thread thread = new Thread(() => 
                Console.WriteLine($"Received message: {message}")
                    );
                thread.Start();
            });
            
            await connection.StartAsync();
            Console.WriteLine("Connection started.");
            
            Console.WriteLine("aaaaaaaa");
            tagProcessingService.InitializeTagThreads();
        }
    }
}