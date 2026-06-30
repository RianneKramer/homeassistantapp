using dashboard_api.Models;

namespace dashboard_api.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}