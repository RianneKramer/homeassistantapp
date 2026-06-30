using dashboard_api.Dtos;
using dashboard_api.Models;

namespace dashboard_api.Interfaces;

public interface IHomeAssistantRestService
{
    Task<Response?> GetApi();
    Task<List<DomainDto>?> GetEntities();
    Task SyncCurrentStates();
    Task CallService(string domain, string action, object payload);
}