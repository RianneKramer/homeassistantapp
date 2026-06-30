using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface ISignalRBroadcastService
{
    Task BroadcastUpdate(EntityDto dto);
}