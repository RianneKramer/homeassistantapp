using dashboard_api.Dtos;
using dashboard_api.Models;

namespace dashboard_api.Interfaces;

public interface ISceneValidationService
{
    Task<ValidationResultDto> ValidateScene(Scene scene);
    Task<ValidationResultDto> ValidateScene(CreateSceneDto dto);
    Task<ValidationResultDto> ValidateScene(
        int sceneId,
        UpdateSceneDto dto);
}