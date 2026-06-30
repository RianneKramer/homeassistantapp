using dashboard_api.Models;
using NodaTime;

namespace dashboard_api.Dtos;

public class UpdateSceneDto
{
    public string Name { get; set; } = string.Empty;

    public bool Enabled { get; set; }

    public TriggerType TriggerType { get; set; }

    public Instant? TriggerAt { get; set; }

    public bool RunOnce { get; set; }

    public List<SceneTriggerDto> Triggers { get; set; } = [];

    public List<SceneActionDto> Actions { get; set; } = [];
}