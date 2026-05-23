using System.Text;
using System.Text.Json;

namespace dashboard_api.Services;

public class HomeAssistantRestService
{
    private readonly HttpClient _httpClient;
    public HomeAssistantRestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ToggleLight(string entityId, bool turnOn)
    {
        var endpoint  = turnOn 
            ? $"api/services/light/turn_on"
            : $"api/services/light/turn_off";

        var payload = new
        {
            entity_id = entityId,
        };
        
        var json = JsonSerializer.Serialize(payload);
        
        await _httpClient.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    public async Task SetBrightness(string entityId, int brightness)
    {
        var payload = new
        {
            entity_id = entityId,
            brightness_pct = brightness
        };

        var json = JsonSerializer.Serialize(payload);
        
        await _httpClient.PutAsync(
            $"api/services/light/brightness",
            new StringContent(json, Encoding.UTF8, "application/json"));
    }
}