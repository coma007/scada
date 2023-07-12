using Newtonsoft.Json;

namespace scada_back.Api.WebSocket;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketHandler
{
    private WebSocket webSocket;
    private static ConcurrentDictionary<string, WebSocket> connectedClients = new ConcurrentDictionary<string, WebSocket>();
    private static Dictionary<string, HashSet<WebSocket>> topicSubscriptions = new Dictionary<string, HashSet<WebSocket>>();

    
    public async Task HandleWebSocketRequest(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            webSocket = await context.WebSockets.AcceptWebSocketAsync();
            Task receiveTask = ReceiveMessage();
            await receiveTask;
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }

    private async Task ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            if (receivedMessage.Contains("subscribe:"))
            {
                string topic = receivedMessage.Substring(10);
                SubscribeToTopic(topic);
            }
            else
            {
                Console.WriteLine(receivedMessage);
            }

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    public async Task SendMessage(string topic, object message)
    {
        if (topicSubscriptions.ContainsKey(topic))
        {
            byte[] buffer = SerializeMessage(topic, message);
            foreach (WebSocket subscriber in topicSubscriptions[topic])
            {
                try
                {
                    await subscriber.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, true,
                        CancellationToken.None);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }
    }
    
    private void SubscribeToTopic(string topic)
    {
        if (!topicSubscriptions.ContainsKey(topic))
        {
            topicSubscriptions[topic] = new HashSet<WebSocket>();
        }
        topicSubscriptions[topic].Add(webSocket);
    }
    
    private byte[] SerializeMessage(string topic, object message)
    {
        string serializedMessage = JsonConvert.SerializeObject(message);
        byte[] buffer = Encoding.UTF8.GetBytes(topic + "=>" + serializedMessage);
        return buffer;
    }

}
