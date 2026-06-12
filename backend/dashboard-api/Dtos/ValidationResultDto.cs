namespace dashboard_api.Dtos;

public class ValidationResultDto
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = [];
}