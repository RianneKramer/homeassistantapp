namespace dashboard_api.Dtos;

public class EnergyOverviewDto
{
    public double CurrentPower { get; set; }
    public double Phase1 { get; set; }
    public double Phase2 { get; set; }
    public double Phase3 { get; set; }
}