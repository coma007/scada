using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Configuration;
using System.Data.Common;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace scada_core.ApiClient;

public class WebSocketClient
{
    private readonly ClientWebSocket _webSocket;

    public WebSocketClient()
    {

        _webSocket = new ClientWebSocket();
        EstablishConnection().Wait();
    }

    private async Task EstablishConnection()
    {
        string webSocketUrl = ConfigurationSettings.AppSettings["WebSocketUrl"];

        await _webSocket.ConnectAsync(new Uri(webSocketUrl), CancellationToken.None);

        Console.WriteLine("WebSocket connection established.");

        await ReceiveWebSocketMessages();

        //var connection = new HubConnectionBuilder()
        //    .WithUrl(webSocketUrl)
        //    .Build();

        //await connection.StartAsync();

        //connection.On<string>("NewTagCreated", tag =>
        //{
        //    Console.WriteLine("New tag created:");
        //    //Console.WriteLine($"Tag Id: {tag.Id}");
        //    //Console.WriteLine($"Tag Name: {tag.Name}");
        //});
    }

    private async Task ReceiveWebSocketMessages()
    {
        var buffer = new byte[1024];
        var arraySegment = new ArraySegment<byte>(buffer);

        while (_webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = await _webSocket.ReceiveAsync(arraySegment, CancellationToken.None);

            // Process the received message
            string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received message: {message}");

            // Clear the buffer
            Array.Clear(buffer, 0, buffer.Length);
        }
    }
}