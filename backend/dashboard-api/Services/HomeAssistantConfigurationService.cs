using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class HomeAssistantConfigurationService(SmartHomeDbContext context) : IHomeAssistantConfigurationService
{
    public async Task<HomeAssistantConfiguration> GetConfigurationAsync()
    {
        var settings = await context.Settings.FirstOrDefaultAsync();

        if (settings == null)
            throw new InvalidOperationException(
                "No Home Assistant configuration found.");

        return new HomeAssistantConfiguration
        {
            Url = settings.HomeAssistantUrl,
            Token = settings.HomeAssistantToken,
            WebSocketUrl = settings.HomeAssistantUrl
                               .Replace("http://", "ws://")
                               .Replace("https://", "wss://")
                           + "/api/websocket"
        };
    }
}