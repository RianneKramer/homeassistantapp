using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Data;

public class SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : DbContext(options)
{
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Scene> Scenes { get; set; }
    public DbSet<SceneTrigger> SceneTriggers { get; set; }
    public DbSet<SceneAction> SceneActions { get; set; }
    
}