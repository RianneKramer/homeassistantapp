using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace dashboard_api.Models;

public class HomeAssistantMessage
{
    [JsonPropertyName("event")]
    public EventData? Event { get; set; }
}

public class EventData
{
    [JsonPropertyName("data")] 
    public StateData Data { get; set; } = new();
}

public class StateData
{
    [JsonPropertyName("entity_id")]
    public string EntityId { get; set; } = "";
    
    [JsonPropertyName("new_state")]
    public EntityState? NewState { get; set; }
}

public class EntityState
{
    [JsonPropertyName("entity_id")]
    public string EntityId { get; set; } = "";
    [JsonPropertyName("state")]
    public string State { get; set; } = "";
    [JsonPropertyName("attributes")]
    public Dictionary<string, JsonElement> Attributes { get; set; } = new();
}