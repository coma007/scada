using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json.Linq;
using scada_core.TagProcessing;

public class WebSocketClient
{
    private const string _logTag = "WS: ";

    private enum WebSocketClientType
    {
        NewTag,
        TagScan,
        TagDelete
    }

    private ClientWebSocket _webSocket;
    
    private async Task CreateClient()
    {
        string url = ConfigurationManager.AppSettings["WebSocketUrl"];

        _webSocket = new ClientWebSocket();
        await _webSocket.ConnectAsync(new Uri(url), CancellationToken.None);
    }

    public async Task ConnectToWebSocket(TagProcessor processor)
    {
        if (_webSocket == null) await CreateClient();
        await SendMessage("subscribe:NewTagCreated");
        await SendMessage("subscribe:TagScanUpdated");
        await SendMessage("subscribe:TagDeleted");

        Task receiveTask = ReceiveMessage(processor);

        await receiveTask;
    }

    private async Task ReceiveMessage(TagProcessor processor)
    {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            string[] tokens = receivedMessage.Split("=>");
            string topic = tokens[0];
            receivedMessage = tokens[1];
            JToken tag = JToken.Parse(receivedMessage);
            Dictionary<string, object> tagData;
            string tagName = TagProcessingService.ExtractProperties(tag, out tagData);
            tagData.Add("tagName", tagName);
            Console.WriteLine(_logTag + $"Received message: {receivedMessage}");
            if (topic.Equals("NewTagCreated")) processor.AddTag(tagData);
            else if (topic.Equals("TagDeleted")) processor.RemoveTag(tagData);
            else processor.ChangeScan(tagData);
            result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await _webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    private async Task SendMessage(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}