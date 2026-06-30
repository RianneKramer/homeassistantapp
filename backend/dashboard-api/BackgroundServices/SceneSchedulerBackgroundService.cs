using System.Text.Json;
using dashboard_api.Data;
using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using dashboard_api.Models;
using dashboard_api.Services;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace dashboard_api.BackgroundServices;

public class SceneSchedulerBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckScheduledScenes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(
                TimeSpan.FromSeconds(30),
                stoppingToken);
        }
    }
    
    private async Task CheckScheduledScenes()
    {
        using var scope = serviceProvider.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<SmartHomeDbContext>();

        var deviceController = scope.ServiceProvider
            .GetRequiredService<DeviceControllerService>();

        var now = SystemClock.Instance.GetCurrentInstant();

        var scenes = await db.Scenes
            .Include(x => x.Actions)
            .Where(x =>
                x.Enabled &&
                x.TriggerType == TriggerType.Scheduled &&
                x.TriggerAt != null)
            .ToListAsync();

        foreach (var scene in scenes)
        {
            await ProcessScene(scene, now, deviceController);
        }

        await db.SaveChangesAsync();
    }
    
    private async Task ProcessScene(Scene scene, Instant now, IDeviceControllerService deviceController)
    {
        if (scene.TriggerAt > now)
            return;

        if (scene.RunOnce &&
            scene.LastExecutedAt != null)
        {
            return;
        }

        foreach (var action in scene.Actions)
        {
            await deviceController.ExecuteAsync(
                new DeviceCommandDto
                {
                    EntityId = action.EntityId,
                    Action = action.Action,
                    Data = ParsePayload(action.PayloadJson)
                });
        }

        scene.LastExecutedAt = now;

        if (scene.RunOnce)
        {
            scene.Enabled = false;
        }
    }
    
    private static Dictionary<string, object>? ParsePayload(
        string? payloadJson)
    {
        if (string.IsNullOrWhiteSpace(payloadJson))
            return null;

        return JsonSerializer.Deserialize<
            Dictionary<string, object>>(payloadJson);
    }
}