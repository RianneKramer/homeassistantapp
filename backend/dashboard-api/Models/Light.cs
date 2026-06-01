namespace dashboard_api.Models;

public class Light
{
    public int Id { get; set; }
    public string EntityId { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public int Brightness { get; set; }
}