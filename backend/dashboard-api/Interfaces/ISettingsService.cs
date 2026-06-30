using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface ISettingsService
{
    Task<SettingsDto> GetAsync();
    Task UpdateAsync(SettingsDto dto);
    Task<bool> TestConnectionAsync(SettingsDto dto);
}