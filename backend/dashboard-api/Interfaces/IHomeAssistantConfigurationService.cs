using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface IHomeAssistantConfigurationService
{
    Task<HomeAssistantConfiguration> GetConfigurationAsync();
}