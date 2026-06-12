namespace dashboard_api.Models;

public class Entity
{
    public int Id { get; set; }
    public string EntityId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string AttributeJson { get; set; } = "{}";
}