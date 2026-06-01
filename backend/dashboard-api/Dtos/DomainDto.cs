using System.Text.Json.Serialization;

namespace dashboard_api.Dtos;

public class DomainDto
{
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("services")] 
    public Dictionary<string, object> Name { get; set; } = [];
}