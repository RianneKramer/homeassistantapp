using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace dashboard_api.Services;

public class HomeAssistantWebSocketService
{
    private readonly IConfiguration _configuration;
    private readonly LightStateService _lightStateService;

    public HomeAssistantWebSocketService(IConfiguration configuration, LightStateService lightStateService)
    {
        _configuration = configuration;
        _lightStateService = lightStateService;
    }

    public async Task StartListening()
    {
        var websocketUrl = _configuration["HomeAssistant:WebSocket"];
        var token = _configuration["HomeAssistant:Token"];

        using var socket = new ClientWebSocket();
        
        await socket.ConnectAsync(new Uri(websocketUrl!), CancellationToken.None);

        var auth = new
        {
            type = "auth",
            access_token = token
        };
        
        await Send(socket, auth);

        await Task.Delay(1000);

        var subscribe = new
        {
            id = 1,
            type = "subscribe_events",
            event_type = "state_changed"
        };

        await Send(socket, subscribe);

        var buffer = new byte[8192];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            HandleMessage(json);
        }
    }

    private async Task Send(ClientWebSocket socket, object payload)
    {
        var json = JsonSerializer.Serialize(payload);

        var bytes = Encoding.UTF8.GetBytes(json);

        await socket.SendAsync(
            new ArraySegment<byte>(bytes),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);
    }

    private void HandleMessage(string json)
    {
        Console.WriteLine(json);
    }
}