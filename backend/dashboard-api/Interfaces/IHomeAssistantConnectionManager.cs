namespace dashboard_api.Interfaces;

public interface IHomeAssistantConnectionManager
{
    void RequestReconnect();
    bool ConsumeReconnectRequest();
}