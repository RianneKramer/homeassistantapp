namespace dashboard_api.Models;

public class LightEntity
{
    public int Id { get; set; }
    public string EntityId { get; set; }
    public string Name { get; set; }
    public bool IsOn { get; set; }
    public int Brightness { get; set; }
}