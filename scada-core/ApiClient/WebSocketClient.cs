using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json.Linq;
using scada_core.TagProcessing;

public class WebSocketClient
{

    private enum WebSocketClientType
    {
        NewTag,
        TagScan
    }

    private ClientWebSocket webSocket;
    private WebSocketClientType type;

    private async Task CreateClient()
    {
        string url = ConfigurationManager.AppSettings["WebSocketUrl"];

        webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);
    }

    public async Task ConnectToNewTagSocket(TagProcessor processor)
    {
        if (webSocket == null) await CreateClient();
        await SendMessage("subscribe:NewTagCreated");
        type = WebSocketClientType.NewTag;

        Task receiveTask = ReceiveMessage(processor);

        await receiveTask;
    }

    public async Task ConnectToUpdatedScanTagSocket(TagProcessor processor)
    {
        if (webSocket == null) await CreateClient();
        await SendMessage("subscribe:TagScanUpdated");
        type = WebSocketClientType.TagScan;

        Task receiveTask = ReceiveMessage(processor);

        await receiveTask;
    }

    private async Task ReceiveMessage(TagProcessor processor)
    {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            JToken tag = JToken.Parse(receivedMessage);
            Dictionary<string, object> tagData;
            string tagName = TagProcessingService.ExtractProperties(tag, out tagData);
            tagData.Add("tagName", tagName);
            Console.WriteLine("Received message: " + receivedMessage);
            if (type == WebSocketClientType.NewTag) processor.AddThread(tagData);
            else processor.ChangeScan(tagData);
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