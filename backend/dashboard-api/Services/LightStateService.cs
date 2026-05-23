using dashboard_api.Models;

namespace dashboard_api.Services;

public class LightStateService(SignalRBroadcastService broadcastService)
{
    private readonly List<LightEntity> _lights = [];

    public IEnumerable<LightEntity> GetLights()
    {
        return _lights;
    }

    public async Task UpdateLight(LightEntity updated)
    {
        var existing = _lights.FirstOrDefault(l => l.Id == updated.Id);
        
        if (existing == null)
            return;

        existing.IsOn = updated.IsOn;
        existing.Brightness = updated.Brightness;
        
        await broadcastService.BroadcastLightUpdate(existing);
    }
}