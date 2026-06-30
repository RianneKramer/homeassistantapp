using System.Text.Json;
using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Mappers;
using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class EntitySyncService(SmartHomeDbContext context, ISignalRBroadcastService broadcastService, EntityMapper mapper) : IEntitySyncService
{
    public async Task SyncAsync(EntityDto dto)
    {
        var entity = await context.Entities
            .FirstOrDefaultAsync(e => e.EntityId == dto.EntityId);

        var attributeJson = JsonSerializer.Serialize(dto.Attributes);

        if (entity != null && entity.State == dto.State && entity.AttributeJson == attributeJson)
        {
            return;
        }

        if (entity == null)
        {
            entity = new Entity
            {
                EntityId = dto.EntityId,
                Name = GetFriendlyName(dto),
                State = dto.State,
                AttributeJson = attributeJson
            };
            context.Entities.Add(entity);
        }
        else
        {
            entity.State = dto.State;
            entity.AttributeJson = attributeJson;

            if (!string.IsNullOrWhiteSpace(GetFriendlyName(dto)))
            {
                entity.Name = GetFriendlyName(dto);
            }
        }
        
        await context.SaveChangesAsync();
        
        await broadcastService.BroadcastUpdate(mapper.ToDto(entity));
    }

    public async Task SyncManyAsync(IEnumerable<EntityDto>? dtos)
    {
        if (dtos == null) return;

        var entityDtos = dtos.ToList();
        
        var existingEntities = await context.Entities.ToListAsync();
        
        var incomingEntityIds = entityDtos
            .Select(x => x.EntityId)
            .ToHashSet();

        var entitiesToDelete = existingEntities
            .Where(x => !incomingEntityIds.Contains(x.EntityId))
            .ToList();
        
        context.Entities.RemoveRange(entitiesToDelete);
        
        foreach (var dto in entityDtos)
        {
            var entity = existingEntities
                .FirstOrDefault(x => x.EntityId == dto.EntityId);
            
            var attributeJson = JsonSerializer.Serialize(dto.Attributes);

            if (entity == null)
            {
                context.Entities.Add(new Entity
                {
                    EntityId = dto.EntityId,
                    Name = GetFriendlyName(dto),
                    State = dto.State,
                    AttributeJson = attributeJson
                });
            }
            else
            {
                entity.State = dto.State;
                entity.AttributeJson = attributeJson;
                entity.Name = GetFriendlyName(dto);
            }
        }
        await context.SaveChangesAsync();
    }

    public async Task<List<EntityDto>> GetEntities()
    {
        var entities = await context.Entities.ToListAsync();

        return entities.Select(mapper.ToDto).ToList();
    }

    private static string GetFriendlyName(EntityDto dto)
    {
        if (dto.Attributes!.TryGetValue("friendly_name", out var value))
        {
            return value.GetString() ?? dto.EntityId;
        }
        return dto.EntityId;
    }
}