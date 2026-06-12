namespace dashboard_api.Models;

public class SceneAction
{
    public int Id { get; set; }

    public int SceneId { get; set; }

    public Scene Scene { get; set; } = null!;

    public string EntityId { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? PayloadJson { get; set; }
}