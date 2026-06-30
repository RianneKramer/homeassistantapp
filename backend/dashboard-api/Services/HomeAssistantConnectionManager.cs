using dashboard_api.Interfaces;

namespace dashboard_api.Services;

public class HomeAssistantConnectionManager : IHomeAssistantConnectionManager
{
    private volatile bool _reconnectRequested;
    public void RequestReconnect()
    {
        _reconnectRequested = true;
    }

    public bool ConsumeReconnectRequest()
    {
        if (!_reconnectRequested)
            return  false;
        
        _reconnectRequested = false;
        return true;
    }
}