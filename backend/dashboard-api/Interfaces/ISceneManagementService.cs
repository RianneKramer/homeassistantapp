using dashboard_api.Dtos;

namespace dashboard_api.Interfaces;

public interface ISceneManagementService
{
    Task<List<SceneResponseDto>> GetScenes();
    Task<SceneResponseDto?> GetScene(int sceneId);
    Task<SceneResponseDto> CreateSceneAsync(CreateSceneDto dto);
    Task<SceneResponseDto?> UpdateSceneAsync(int sceneId, UpdateSceneDto dto);
    Task<bool> DeleteSceneAsync(int id);
    Task<bool> EnableSceneAsync(int id);
    Task<bool> DisableSceneAsync(int id);
}