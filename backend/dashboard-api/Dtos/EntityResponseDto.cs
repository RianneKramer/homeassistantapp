using System.Text.Json;

namespace dashboard_api.Dtos;

public class EntityResponseDto
{
    public Dictionary<string, JsonElement>? Attributes { get; set; } = [];
    
    public string EntityId { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
}