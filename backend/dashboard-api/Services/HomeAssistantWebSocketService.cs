using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using dashboard_api.Dtos;
using dashboard_api.Models;

namespace dashboard_api.Services;

public class HomeAssistantWebSocketService(IConfiguration configuration, EntitySyncService syncService)
{

    public async Task StartListening()
    {
        var websocketUrl = configuration["HomeAssistant:WebSocket"];
        var token = configuration["HomeAssistant:Token"];

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

            await HandleMessage(json);
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

    private async Task HandleMessage(string json)
    {
        var message = JsonSerializer.Deserialize<HomeAssistantMessage>(json);

        var newState = message?.Event?.Data.NewState;

        if (newState == null)
            return;

        var dto = new EntityDto
        {
            EntityId = newState.EntityId,
            State = newState.State,
            Attributes = newState.Attributes
        };

        await syncService.SyncAsync(dto);
    }
}