namespace dashboard_api.Interfaces;

public interface IHomeAssistantWebSocketService
{
    Task StartListening(CancellationToken cancellationToken);
    
}