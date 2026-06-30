using dashboard_api.Interfaces;

namespace dashboard_api.BackgroundServices;

public class HomeAssistantListener(IServiceProvider serviceProvider, IHomeAssistantConnectionManager connectionManager) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            
            var websocketService = scope.ServiceProvider.GetRequiredService<IHomeAssistantWebSocketService>();

            try
            {
                await websocketService.StartListening(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (connectionManager.ConsumeReconnectRequest())
            {
                Console.WriteLine("Reconnected...");
                continue;
            }
            
            await Task.Delay(50000, stoppingToken);
        }
    }
}