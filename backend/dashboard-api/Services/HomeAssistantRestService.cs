using System.Net.Http.Headers;
using System.Text.Json;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Models;

namespace dashboard_api.Services;

public class HomeAssistantRestService(IEntitySyncService syncService, IHomeAssistantConfigurationService configurationService, HttpClient httpClient) : IHomeAssistantRestService
{
    private async Task ConfigureClientAsync()
    {
        var config = await configurationService.GetConfigurationAsync();
        
        httpClient.BaseAddress = new Uri(config.Url);
        
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", config.Token);
    }
    
    
    public async Task<Response?> GetApi()
    {
        await ConfigureClientAsync();
        
        var response = await httpClient.GetAsync("/api/");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(json);
    }

    public async Task<List<DomainDto>?> GetEntities()
    {
        await ConfigureClientAsync();
        
        var response = await httpClient.GetAsync("/api/services");
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine(json);
        return JsonSerializer.Deserialize<List<DomainDto>>(json);
    }
    
    public async Task SyncCurrentStates()
    {
        await ConfigureClientAsync();
        
        var entities = await httpClient.GetFromJsonAsync<List<EntityDto>>("/api/states");

        if (entities == null)
            return;

        await syncService.SyncManyAsync(entities);
    }
    
    public async Task CallService(string domain, string action, object payload)
    {
        await ConfigureClientAsync();
        
        await httpClient.PostAsJsonAsync(
            $"/api/services/{domain}/{action}",
            payload);
    }
}