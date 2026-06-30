using dashboard_api.Data;
using dashboard_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Services;

public class LoginService(SmartHomeDbContext context, ITokenService tokenService) : ILoginService
{
    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Username == username);
        
        if (user == null)
            return  null;
        
        if (user.Password != password)
            return  null;

        return tokenService.GenerateToken(user);
    }
}