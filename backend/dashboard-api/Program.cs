using System.Net.Http.Headers;
using dashboard_api;
using dashboard_api.BackgroundServices;
using dashboard_api.Data;
using dashboard_api.Handlers;
using dashboard_api.Hubs;
using dashboard_api.Interfaces;
using dashboard_api.Mappers;
using dashboard_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SmartHomeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"), o => o.UseNodaTime())
    );

builder.Services.AddScoped<HomeAssistantRestService>();

builder.Services.AddSingleton<SignalRBroadcastService>();
builder.Services.AddScoped<HomeAssistantWebSocketService>();
builder.Services.AddScoped<HomeAssistantHub>();
builder.Services.AddScoped<EntitySyncService>();
builder.Services.AddScoped<DeviceControllerService>();

builder.Services.AddScoped<IDeviceDomainHandler, LightHandler>();

builder.Services.AddScoped<SceneMapper>();

builder.Services.AddScoped<ISceneManagementService, SceneManagementService>();
builder.Services.AddScoped<ISceneValidationService, SceneValidationService>();

builder.Services.AddHostedService<HomeAssistantListener>();
builder.Services.AddHostedService<SceneSchedulerBackgroundService>();

builder.Services.AddHttpClient<HomeAssistantRestService>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["HomeAssistant:Url"]!);
    client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", config["HomeAssistant:Token"]);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var syncServices = scope.ServiceProvider.GetRequiredService<HomeAssistantRestService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();