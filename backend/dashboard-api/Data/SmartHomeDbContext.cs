using dashboard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard_api.Data;

public class SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : DbContext(options)
{
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Scene> Scenes { get; set; }
    public DbSet<SceneTrigger> SceneTriggers { get; set; }
    public DbSet<SceneAction> SceneActions { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Settings>().HasData(
            new Settings
            {
                Id = 1,
                HomeAssistantUrl = "http://192.168.2.29:8123",
                HomeAssistantToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiIwNzc5NDdkMDAxNTc0OGQxYTVlZWU5NjFhNmI2OWUyYSIsImlhdCI6MTc4MTI4NjI3MCwiZXhwIjoyMDk2NjQ2MjcwfQ.G2UM-uBbmhsK8BTpIHLLto8bau6qsYGVhxGaprZbmSA"
            }
        );
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
            }
        );
    }
    
}