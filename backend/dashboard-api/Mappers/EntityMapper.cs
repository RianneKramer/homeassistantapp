using System.Text.Json;
using dashboard_api.Dtos;
using dashboard_api.Models;

namespace dashboard_api.Mappers;

public class EntityMapper
{
    public EntityDto ToDto(Entity entity)
    {
        return new EntityDto
        {
            EntityId = entity.EntityId,
            State = entity.State,
            Attributes = string.IsNullOrWhiteSpace(entity.AttributeJson)
                ? null
                : JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(entity.AttributeJson)
        };
    }
}