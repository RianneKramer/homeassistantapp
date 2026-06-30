using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class EnergyOverviewService(SmartHomeDbContext context) : IEnergyOverviewService
{
    public async Task<EnergyOverviewDto> GetOverviewAsync()
    {
        var entities = await context.Entities.ToListAsync();

        return new EnergyOverviewDto
        {
            CurrentPower = GetValue(entities, "sensor.p1_meter_vermogen"),
            Phase1 = GetValue(entities, "sensor.p1_meter_vermogen_fase_1"),
            Phase2 = GetValue(entities, "sensor.p1_meter_vermogen_fase_2"),
            Phase3 = GetValue(entities, "sensor.p1_meter_vermogen_fase_3")
        };
    }

    private double GetValue(IEnumerable<Entity> entities, string entityId)
    {
        var entity = entities.FirstOrDefault(e => e.EntityId == entityId);

        return double.TryParse(entity?.State, out var value) ? value : 0;
    }
}