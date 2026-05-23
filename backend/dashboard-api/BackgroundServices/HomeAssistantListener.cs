using dashboard_api.Services;

namespace dashboard_api.BackgroundServices;

public class HomeAssistantListener : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    
    public HomeAssistantListener(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var websocketService = scope
            .ServiceProvider
            .GetRequiredService<HomeAssistantWebSocketService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await websocketService.StartListening();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}