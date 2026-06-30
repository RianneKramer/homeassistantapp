using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface IEnergyOverviewService
{
    Task<EnergyOverviewDto> GetOverviewAsync();
}