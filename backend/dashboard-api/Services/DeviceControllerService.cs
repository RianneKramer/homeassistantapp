using dashboard_api.Dtos;
using dashboard_api.Interfaces;

namespace dashboard_api.Services;

public class DeviceControllerService : IDeviceControllerService
{
    private readonly Dictionary<string, IDeviceDomainHandler> _handlers;

    public DeviceControllerService(IEnumerable<IDeviceDomainHandler> handlers)
    {
        _handlers = handlers.ToDictionary(
            h => h.Domain,
            StringComparer.OrdinalIgnoreCase);
    }
    
    public async Task ExecuteAsync(DeviceCommandDto command)
    {
        var domain = command.EntityId.Split('.')[0];

        if (!_handlers.TryGetValue(domain, out var handler))
        {
            throw new NotSupportedException($"Domain '{domain}' is not supported");
        }
        
        await handler.ExecuteAsync(command);
    }
}