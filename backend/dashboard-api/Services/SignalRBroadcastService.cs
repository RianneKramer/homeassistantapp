using dashboard_api.Dtos;
using dashboard_api.Hubs;
using dashboard_api.Models;
using Microsoft.AspNetCore.SignalR;

namespace dashboard_api.Services;

public class SignalRBroadcastService(IHubContext<HomeAssistantHub> hubContext)
{
    public async Task BroadcastUpdate(Entity dto)
    {
        await hubContext.Clients.All.SendAsync("EntityUpdated", dto);
    }
}