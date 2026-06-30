using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface IEntitySyncService
{
    Task SyncAsync(EntityDto dto);
    Task SyncManyAsync(IEnumerable<EntityDto>? dtos);
    Task<List<EntityDto>> GetEntities();
}