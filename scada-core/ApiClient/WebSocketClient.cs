using System;
using System.Configuration;
using System.Net.WebSockets;
using System.Text;

public class WebSocketClient
{
    private ClientWebSocket webSocket;

    public async Task Connect()
    {
        string url = ConfigurationManager.AppSettings["WebSocketUrl"];
        
        webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);
        await SendMessage("subscribe:NewTag");

        Task receiveTask = ReceiveMessage();

        await receiveTask;
    }

    private async Task ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine("Received message: " + receivedMessage);
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    private async Task SendMessage(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}