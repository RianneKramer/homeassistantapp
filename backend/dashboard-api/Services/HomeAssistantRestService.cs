using System.Text;
using System.Text.Json;
using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class HomeAssistantRestService(HttpClient httpClient, SmartHomeDbContext context, LightStateService lightStateService)
{
    public async Task<Response?> GetApi()
    {
        var response = await httpClient.GetAsync("/api/");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(json);
    }

    public async Task<List<DomainDto>?> GetEntities()
    {
        var response = await httpClient.GetAsync("/api/services");
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine(json);
        return JsonSerializer.Deserialize<List<DomainDto>>(json);
    }

    public async Task LightUpdate(string entityId, string state, int? brightnessPct = null)
    {
        var endpoint = state.Equals("on", StringComparison.OrdinalIgnoreCase)
            ? "api/services/light/turn_on"
            : "api/services/light/turn_off";

        object payload = state.Equals("on", StringComparison.OrdinalIgnoreCase) 
            ? new
            {
                entity_id = entityId,
                brightness_pct = brightnessPct 
            } 
            : new
            {
                entity_id = entityId
            };

        var json = JsonSerializer.Serialize(payload);

        await httpClient.PostAsync(
            endpoint,
            new StringContent(json, Encoding.UTF8, "application/json"));
    }
    
    public async Task SyncCurrentStates()
    {
        var entities = await httpClient.GetFromJsonAsync<List<EntityDto>>("/api/states");

        if (entities == null)
            return;
        
        var lights = entities
            .Where(e => e.EntityId.StartsWith("light."))
            .ToList();

        await lightStateService.SyncLight(lights);
    }
}