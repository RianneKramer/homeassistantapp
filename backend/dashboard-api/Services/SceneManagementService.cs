using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Mappers;
using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace dashboard_api.Services;

public class SceneManagementService(SmartHomeDbContext context, SceneMapper mapper, ISceneValidationService validationService, DeviceControllerService deviceController) : ISceneManagementService
{
    public async Task<List<SceneResponseDto>> GetScenes()
    {
        var scenes = await context.Scenes
            .Include(x => x.Triggers)
            .Include(x => x.Actions)
            .ToListAsync();

        return scenes
            .Select(mapper.ToDto)
            .ToList();
    }

    public async Task<SceneResponseDto?> GetScene(int id)
    {
        var scene = await context.Scenes
            .Include(x => x.Triggers)
            .Include(x => x.Actions)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return scene == null ? null : mapper.ToDto(scene);
    }

    public async Task<SceneResponseDto> CreateSceneAsync(CreateSceneDto dto)
    {
        var validation = await validationService.ValidateScene(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(string.Join(",", validation.Errors));
        }
        
        var scene = new Scene
        {
            Name = dto.Name,
            Enabled = true,
            TriggerType = dto.TriggerType,
            TriggerAt =  dto.TriggerAt,
            RunOnce =  dto.RunOnce,
            
            Triggers = dto.Triggers.Select(t => new SceneTrigger
            {
                EntityId = t.EntityId,
                Operator = t.Operator,
                Value = t.Value
            }).ToList()
            ,
            Actions = dto.Actions.Select(a => new SceneAction
            {
                EntityId = a.EntityId,
                Action = a.Action,
                PayloadJson = a.Payload == null
                    ? null
                    : JsonSerializer.Serialize(a.Payload)
            }).ToList()
        };
        context.Scenes.Add(scene);
        await context.SaveChangesAsync();
        return mapper.ToDto(scene);
    }

    public async Task<SceneResponseDto?> UpdateSceneAsync(int id, UpdateSceneDto dto)
    {
        var validation = await validationService.ValidateScene(id, dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(string.Join(",", validation.Errors));
        }
        
        var scene = await context.Scenes
            .Include(x => x.Triggers)
            .Include(x => x.Actions)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (scene == null)
            return null;
        
        scene.Name = dto.Name;
        scene.Enabled = dto.Enabled;
        scene.TriggerType = dto.TriggerType;
        scene.TriggerAt = dto.TriggerAt;
        scene.RunOnce = dto.RunOnce;
        
        context.SceneTriggers.RemoveRange(scene.Triggers);
        context.SceneActions.RemoveRange(scene.Actions);

        scene.Triggers = dto.Triggers
            .Select(t => new SceneTrigger
            {
                EntityId = t.EntityId,
                Operator = t.Operator,
                Value = t.Value
            }).ToList();

        scene.Actions = dto.Actions
            .Select(a => new SceneAction
            {
                EntityId = a.EntityId,
                Action = a.Action,
                PayloadJson = a.Payload == null ? null : JsonSerializer.Serialize(a.Payload)
            }).ToList();
        
        await context.SaveChangesAsync();
        return mapper.ToDto(scene);
    }

    public async Task<bool> DeleteSceneAsync(int id)
    {
        var scene = await context.Scenes.FindAsync(id);

        if (scene == null)
            return false;
        
        context.Scenes.Remove(scene);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EnableSceneAsync(int id)
    {
        var scene = await context.Scenes.FindAsync(id);
        
        if (scene == null)
            return false;
        
        scene.Enabled = true;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DisableSceneAsync(int id)
    {
        var scene = await context.Scenes.FindAsync(id);
        
        if (scene == null)
            return false;
        
        scene.Enabled = false;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task ExecuteScene(int sceneId)
    {
        var scene = await context.Scenes
            .Include(x => x.Actions)
            .FirstOrDefaultAsync(x => x.Id == sceneId);

        if (scene == null)
        {
            throw new KeyNotFoundException($"Scene with id {sceneId} not found");
        }

        foreach (var action in scene.Actions)
        {
            await deviceController.ExecuteAsync(
                new DeviceCommandDto
                {
                    EntityId = action.EntityId,
                    Action = action.Action,
                    Data = string.IsNullOrWhiteSpace(action.PayloadJson)
                        ? null
                        : JsonSerializer.Deserialize<Dictionary<string, object>>(action.PayloadJson)
                });
        }

        scene.LastExecutedAt = SystemClock.Instance.GetCurrentInstant();
        
        await context.SaveChangesAsync();
    }
}