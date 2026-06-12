using System.Text.Json;
using dashboard_api.Dtos;
using dashboard_api.Models;

namespace dashboard_api.Mappers;

public class SceneMapper
{
    public SceneResponseDto ToDto(Scene scene)
    {
        return new SceneResponseDto
        {
            Id = scene.Id,
            Name = scene.Name,
            Enabled = scene.Enabled,
            TriggerType = scene.TriggerType,
            TriggerAt = scene.TriggerAt,
            RunOnce = scene.RunOnce,
            LastExecutedAt = scene.LastExecutedAt,

            Triggers = scene.Triggers
                .Select(t => new SceneTriggerDto
                {
                    Id = t.Id,
                    EntityId = t.EntityId,
                    Operator = t.Operator,
                    Value = t.Value
                })
                .ToList(),

            Actions = scene.Actions
                .Select(a => new SceneActionDto
                {
                    Id = a.Id,
                    EntityId = a.EntityId,
                    Action = a.Action,
                    Payload = string.IsNullOrWhiteSpace(a.PayloadJson)
                        ? null
                        : JsonSerializer.Deserialize<Dictionary<string, object>>(a.PayloadJson)
                })
                .ToList()
        };
    }
}