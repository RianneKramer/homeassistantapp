using dashboard_api.Dtos;
using dashboard_api.Hubs;
using dashboard_api.Models;
using Microsoft.AspNetCore.SignalR;

namespace dashboard_api.Services;

public class SignalRBroadcastService
{
    private readonly IHubContext<HomeAssistantHub> _hubContext;
    
    public SignalRBroadcastService(IHubContext<HomeAssistantHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task BroadcastLightUpdate(LightDto light)
    {
        await _hubContext.Clients.All.SendAsync("LightUpdated", light);
    }
}