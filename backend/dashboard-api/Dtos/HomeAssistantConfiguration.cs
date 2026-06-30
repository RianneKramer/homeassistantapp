namespace dashboard_api.Dtos;

public class HomeAssistantConfiguration
{
    public string Url { get; set; } = string.Empty;
    public string WebSocketUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}