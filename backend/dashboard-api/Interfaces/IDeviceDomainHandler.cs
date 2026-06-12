using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface IDeviceDomainHandler
{
    string Domain { get; }
    Task ExecuteAsync(DeviceCommandDto command);
}