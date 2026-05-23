using dashboard_api.Hubs;
using dashboard_api.Models;
using Microsoft.AspNetCore.SignalR;

namespace dashboard_api.Services;

public class SignalRBroadcastService
{
    private readonly IHubContext<LightHub> _hubContext;
    
    public SignalRBroadcastService(IHubContext<LightHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task BroadcastLightUpdate(LightEntity light)
    {
        await _hubContext.Clients.All.SendAsync("LightUpdated", light);
    }
}