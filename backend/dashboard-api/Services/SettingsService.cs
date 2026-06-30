using System.Net.Http.Headers;
using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class SettingsService(SmartHomeDbContext context, IHomeAssistantConnectionManager connectionManager) : ISettingsService
{
    public async Task<SettingsDto> GetAsync()
    {
        var settings = await context.Settings.FirstOrDefaultAsync();

        if (settings == null)
        {
            settings = new Settings
            {
                HomeAssistantUrl = "http://192.168.2.29:8123",
                HomeAssistantToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiIwNzc5NDdkMDAxNTc0OGQxYTVlZWU5NjFhNmI2OWUyYSIsImlhdCI6MTc4MTI4NjI3MCwiZXhwIjoyMDk2NjQ2MjcwfQ.G2UM-uBbmhsK8BTpIHLLto8bau6qsYGVhxGaprZbmSA"
            };

            context.Settings.Add(settings);
            await context.SaveChangesAsync();
        }

        return new SettingsDto
        {
            homeAssistantUrl = settings.HomeAssistantUrl,
            homeAssistantToken = settings.HomeAssistantToken
        };
    }

    public async Task UpdateAsync(SettingsDto dto)
    {
        var settings = await context.Settings.FirstAsync();
        
        settings.HomeAssistantUrl = dto.homeAssistantUrl;
        settings.HomeAssistantToken = dto.homeAssistantToken;
        
        await context.SaveChangesAsync();
        
        connectionManager.RequestReconnect();
    }

    public async Task<bool> TestConnectionAsync(SettingsDto dto)
    {
        using var client = new HttpClient();

        client.BaseAddress = new Uri(dto.homeAssistantUrl);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer", dto.homeAssistantToken);

        try
        {
            var response = await client.GetAsync("/api/");
            
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}