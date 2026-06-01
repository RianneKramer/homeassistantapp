using System.Text.Json.Serialization;

namespace dashboard_api.Models;

public class Response
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}