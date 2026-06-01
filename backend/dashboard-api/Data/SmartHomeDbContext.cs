using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Data;

public class SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : DbContext(options)
{
    public DbSet<Light> Lights { get; set; }
}