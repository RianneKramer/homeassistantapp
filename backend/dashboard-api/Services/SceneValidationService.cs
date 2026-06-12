using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace dashboard_api.Services;

public class SceneValidationService(SmartHomeDbContext context) : ISceneValidationService
{
    public async Task<ValidationResultDto> ValidateScene(Scene scene)
    {
        throw new NotImplementedException();
    }

    public async Task<ValidationResultDto> ValidateScene(CreateSceneDto dto)
    {
        var result = new ValidationResultDto();

        result.Errors.AddRange(
            ValidateScheduledTrigger(dto));

        result.Errors.AddRange(
            ValidateDuplicateEntities(dto.Actions));

        result.Errors.AddRange(
            await ValidateEntitiesExist(dto.Actions));

        if (dto.TriggerType == TriggerType.Scheduled &&
            dto.TriggerAt != null)
        {
            result.Errors.AddRange(
                await ValidateScheduledScene(
                    dto.TriggerAt.Value,
                    dto.Actions));
        }

        result.IsValid = result.Errors.Count == 0;

        return result;
    }

    public async Task<ValidationResultDto> ValidateScene(int sceneId, UpdateSceneDto dto)
    {
        var result = new ValidationResultDto();
        
        result.Errors.AddRange(
            ValidateDuplicateEntities(dto.Actions));

        result.Errors.AddRange(
            await ValidateEntitiesExist(dto.Actions));

        if (dto.TriggerType == TriggerType.Scheduled &&
            dto.TriggerAt != null)
        {
            result.Errors.AddRange(
                await ValidateScheduledScene(
                    dto.TriggerAt.Value,
                    dto.Actions));
        }

        result.IsValid = result.Errors.Count == 0;

        return result;
    }
    
    private async Task<List<string>> ValidateScheduledScene(
        Instant triggerAt,
        IEnumerable<SceneActionDto> actions,
        int? currentSceneId = null)
    {
        var errors = new List<string>();

        var entityIds = actions
            .Select(a => a.EntityId)
            .Distinct()
            .ToList();

        var conflictingScenes = await context.Scenes
            .Include(x => x.Actions)
            .Where(x =>
                x.Enabled &&
                x.TriggerType == TriggerType.Scheduled &&
                x.TriggerAt == triggerAt)
            .ToListAsync();

        if (currentSceneId.HasValue)
        {
            conflictingScenes = conflictingScenes
                .Where(x => x.Id != currentSceneId.Value)
                .ToList();
        }

        foreach (var scene in conflictingScenes)
        {
            foreach (var action in scene.Actions)
            {
                if (entityIds.Contains(action.EntityId))
                {
                    errors.Add(
                        $"Entity '{action.EntityId}' wordt al gebruikt door scene '{scene.Name}'.");
                }
            }
        }

        return errors;
    }
    
    private static List<string> ValidateDuplicateEntities(IEnumerable<SceneActionDto> actions)
    {
        var errors = new List<string>();

        var duplicates = actions
            .GroupBy(x => x.EntityId)
            .Where(x => x.Count() > 1);

        foreach (var duplicate in duplicates)
        {
            errors.Add(
                $"Entity '{duplicate.Key}' komt meerdere keren voor in dezelfde scene.");
        }

        return errors;
    }
    
    private static List<string> ValidateScheduledTrigger(CreateSceneDto dto)
    {
        var errors = new List<string>();

        if (dto.TriggerType != TriggerType.Scheduled)
            return errors;

        if (dto.TriggerAt == null)
        {
            errors.Add(
                "TriggerAt is verplicht voor scheduled scenes.");
        }

        return errors;
    }
    
    private async Task<List<string>> ValidateEntitiesExist( IEnumerable<SceneActionDto> actions)
    {
        var errors = new List<string>();

        var entityIds = actions
            .Select(x => x.EntityId)
            .Distinct()
            .ToList();

        var existingEntities = await context.Entities
            .Where(x => entityIds.Contains(x.EntityId))
            .Select(x => x.EntityId)
            .ToListAsync();

        foreach (var entityId in entityIds)
        {
            if (!existingEntities.Contains(entityId))
            {
                errors.Add(
                    $"Entity '{entityId}' bestaat niet.");
            }
        }

        return errors;
    }
}