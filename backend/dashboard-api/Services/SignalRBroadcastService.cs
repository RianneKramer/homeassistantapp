using dashboard_api.Dtos;
using dashboard_api.Hubs;
using dashboard_api.Interfaces;
using dashboard_api.Models;
using Microsoft.AspNetCore.SignalR;

namespace dashboard_api.Services;

public class SignalRBroadcastService(IHubContext<HomeAssistantHub> hubContext) : ISignalRBroadcastService
{
    public async Task BroadcastUpdate(EntityDto dto)
    {
        await hubContext.Clients.All.SendAsync("EntityUpdated", dto);
    }
}