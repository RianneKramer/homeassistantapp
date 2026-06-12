namespace dashboard_api.Dtos;

public class SceneActionDto
{
    public int Id { get; set; }

    public string EntityId { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public Dictionary<string, object>? Payload { get; set; }
}