namespace dashboard_api.Dtos;

public class SceneTriggerDto
{
    public int Id { get; set; }

    public string EntityId { get; set; } = string.Empty;

    public string Operator { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}