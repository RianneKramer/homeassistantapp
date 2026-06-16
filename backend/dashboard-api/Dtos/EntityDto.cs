
using System.Text.Json;
using System.Text.Json.Serialization;

namespace dashboard_api.Dtos;

public class EntityDto
{
    [JsonPropertyName("attributes")]
    public Dictionary<string, JsonElement>? Attributes { get; set; } = [];
    
    [JsonPropertyName("entity_id")]
    public string EntityId { get; set; } = string.Empty;
    
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    public int Id  { get; set; }
}