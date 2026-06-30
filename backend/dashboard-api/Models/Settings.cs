namespace dashboard_api.Models;

public class Settings
{
    public int Id { get; set; }
    public string HomeAssistantUrl { get; set; } = string.Empty;
    public string HomeAssistantToken { get; set; } = string.Empty;
}