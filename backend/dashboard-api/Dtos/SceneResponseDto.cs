using dashboard_api.Models;
using NodaTime;

namespace dashboard_api.Dtos;

public class SceneResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public TriggerType TriggerType { get; set; }
    public Instant? TriggerAt { get; set; }
    public bool RunOnce { get; set; }
    public Instant? LastExecutedAt { get; set; }
    public List<SceneTriggerDto> Triggers { get; set; } = [];
    public List<SceneActionDto> Actions { get; set; } = [];
}