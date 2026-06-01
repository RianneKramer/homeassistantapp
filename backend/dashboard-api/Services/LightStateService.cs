using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Hubs;
using dashboard_api.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class LightStateService(HomeAssistantHub hubContext, SmartHomeDbContext context, SignalRBroadcastService broadcastService)
{
    private readonly List<Light> _lights = [];

    public IEnumerable<Light> GetLights()
    {
        return _lights;
    }

    public async Task SyncLight(IEnumerable<EntityDto> lightEntities)
    {
        var lights = lightEntities
            .Where(l => l.EntityId.StartsWith("light."))
            .ToList();
        
        // Preload
        var entityIds = lights
            .Select(l => l.EntityId)
            .ToList();

        var dbLights = await context.Lights
            .Where(l => entityIds.Contains(l.EntityId))
            .ToListAsync();
        
        var dbLightLookup = dbLights.ToDictionary(l => l.EntityId, StringComparer.OrdinalIgnoreCase);

        var newLights = new List<Light>();
        var updatedLights = new List<Light>();

        foreach (var light in lights)
        {
            if (dbLightLookup.TryGetValue(light.EntityId, out var dbLight))
            {
                dbLight.State = light.State;

                if (light.State.Equals("on", StringComparison.OrdinalIgnoreCase) &&
                    light.Attributes.TryGetValue("brightness", out var brightness))
                {
                    dbLight.Brightness = brightness.GetInt32();
                }
                
                updatedLights.Add(dbLight);
            } 
            else
            {
                var newLight = new Light
                {
                    EntityId = light.EntityId,
                    State = light.State,
                    Brightness = 255, // Max
                    Name = light.Attributes.TryGetValue("friendly_name", out var friendlyName)
                        ? friendlyName.ToString() 
                        : light.EntityId
                };

                if (light.State.Equals("on", StringComparison.OrdinalIgnoreCase) &&
                    light.Attributes.TryGetValue("brightness", out var brightness))
                {
                    newLight.Brightness = brightness.GetInt32();
                }
                
                newLights.Add(newLight);
            
                Console.WriteLine("New light: " + newLight.Name);
            }
        }

        if (newLights.Count > 0)
        {
            context.Lights.AddRange(newLights);
        }
        
        await context.SaveChangesAsync();

        if (updatedLights.Count > 0)
        {
            foreach (var light in updatedLights)
            {
                await broadcastService.BroadcastLightUpdate(new LightDto
                {
                    Id = light.Id,
                    EntityId = light.EntityId,
                    State = light.State,
                    Brightness = light.Brightness,
                    Name = light.Name,
                });
            }
        }
    }
}