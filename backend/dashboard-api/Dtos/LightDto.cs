namespace dashboard_api.Dtos;

public class LightDto
{
    public int Id { get; set; }
    public string EntityId { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public int Brightness { get; set; }
}