using System.Text.Json;
using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Models;

namespace dashboard_api.Services;

public class HomeAssistantRestService(HttpClient httpClient, SmartHomeDbContext context, IEntitySyncService syncService)
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
    
    public async Task SyncCurrentStates()
    {
        var entities = await httpClient.GetFromJsonAsync<List<EntityDto>>("/api/states");

        if (entities == null)
            return;

        await syncService.SyncManyAsync(entities);
    }
    
    public async Task CallService(string domain, string action, object payload)
    {
        await httpClient.PostAsJsonAsync(
            $"/api/services/{domain}/{action}",
            payload);
    }
}