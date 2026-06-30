using System.Text;
using dashboard_api.BackgroundServices;
using dashboard_api.Data;
using dashboard_api.Handlers;
using dashboard_api.Hubs;
using dashboard_api.Interfaces;
using dashboard_api.Mappers;
using dashboard_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "SmartHomeApi",
            ValidAudience = "SmartHomeClient",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-string-secret-at-least-256-bits-long"))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT ERROR: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SmartHomeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"), o => o.UseNodaTime())
    );

builder.Services.AddScoped<IHomeAssistantConfigurationService, HomeAssistantConfigurationService>();

builder.Services.AddHttpClient<IHomeAssistantRestService, HomeAssistantRestService>();

builder.Services.AddSingleton<ISignalRBroadcastService, SignalRBroadcastService>();
builder.Services.AddScoped<IHomeAssistantWebSocketService, HomeAssistantWebSocketService>();
builder.Services.AddScoped<HomeAssistantHub>();
builder.Services.AddScoped<IEntitySyncService, EntitySyncService>();
builder.Services.AddScoped<IDeviceControllerService,  DeviceControllerService>();

builder.Services.AddScoped<IDeviceDomainHandler, LightHandler>();

builder.Services.AddScoped<SceneMapper>();
builder.Services.AddScoped<EntityMapper>();

builder.Services.AddScoped<IEnergyOverviewService, EnergyOverviewService>();

builder.Services.AddScoped<ISceneManagementService, SceneManagementService>();
builder.Services.AddScoped<ISceneValidationService, SceneValidationService>();

builder.Services.AddScoped<ISettingsService, SettingsService>();

builder.Services.AddSingleton<IHomeAssistantConnectionManager, HomeAssistantConnectionManager>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddHostedService<HomeAssistantListener>();
builder.Services.AddHostedService<SceneSchedulerBackgroundService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy
            .WithOrigins("http://192.168.2.26:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var syncServices = scope.ServiceProvider.GetRequiredService<IHomeAssistantRestService>();
    var db = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
    var retries = 10;

    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch 
        {
            retries--;
            Thread.Sleep(5000);

            if (retries == 0)
            {
                throw;
            }
        }
    }

    await syncServices.SyncCurrentStates();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.MapHub<HomeAssistantHub>("/hub");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();