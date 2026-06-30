namespace dashboard_api.Models;

public class SceneTrigger
{
    public int Id { get; set; }

    public int SceneId { get; set; }

    public Scene Scene { get; set; } = null!;

    public string EntityId { get; set; } = string.Empty;

    public string Operator { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}