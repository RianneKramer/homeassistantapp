using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Services;

namespace dashboard_api.Handlers;

public class LightHandler(IHomeAssistantRestService restService) : IDeviceDomainHandler
{
    public string Domain => "light";
    
    public async Task ExecuteAsync(DeviceCommandDto command)
    {
        var payload = new Dictionary<string, object>
        {
            ["entity_id"] = command.EntityId,
        };

        if (command.Data != null)
        {
            foreach (var item in command.Data)
            {
                payload[item.Key] = item.Value;
            }
        }
        
        await restService.CallService(Domain, command.Action, payload);
    }
}