namespace dashboard_api.Interfaces;

public interface ILoginService
{
    public Task<string?> LoginAsync(string username, string password);
}