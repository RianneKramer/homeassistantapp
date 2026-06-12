using NodaTime;

namespace dashboard_api.Models;

public class Scene
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public TriggerType TriggerType { get; set; }
    public Instant? TriggerAt { get; set; }
    public bool RunOnce { get; set; }
    public Instant? LastExecutedAt { get; set; }
    public List<SceneTrigger> Triggers { get; set; }
    public List<SceneAction> Actions { get; set; }
}