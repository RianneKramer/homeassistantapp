namespace dashboard_api.Dtos;

public class DeviceCommandDto
{
    public string EntityId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public Dictionary<string, object>? Data { get; set; } 
}