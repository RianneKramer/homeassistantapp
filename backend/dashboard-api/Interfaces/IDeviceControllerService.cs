using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface IDeviceControllerService
{
    Task ExecuteAsync(DeviceCommandDto command);
}