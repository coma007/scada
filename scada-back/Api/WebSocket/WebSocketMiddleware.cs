namespace scada_back.Api.WebSocket;

public static class WebSocketMiddleware
{
    public static Func<HttpContext, Func<Task>, Task> Middleware()
    {
        return async (context, next) =>
        {
            if (context.Request.Path == "/Api/Websocket")
            {
                WebSocketHandler webSocketHandler = new WebSocketHandler();
                await webSocketHandler.HandleWebSocketRequest(context);
            }
            else
            {
                await next();
            }
        };
    }
}