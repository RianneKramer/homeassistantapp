using dashboard_api.BackgroundServices;
using dashboard_api.Hubs;
using dashboard_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSignalR();

builder.Services.AddSingleton<SignalRBroadcastService>();
builder.Services.AddSingleton<LightStateService>();
builder.Services.AddSingleton<HomeAssistantWebSocketService>();

builder.Services.AddHostedService<HomeAssistantListener>();

builder.Services.AddHttpClient<HomeAssistantRestService>(client =>
{
    client.BaseAddress = new Uri("http://homeassistant.local:8123");

    client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiI3Zjc0NDZhM2EzNDc0Y2U3YTIyMWE2ZjBhODZlMWExNiIsImlhdCI6MTc3OTU0Mjc2NiwiZXhwIjoyMDk0OTAyNzY2fQ.eTtKMviZcwDHfxSNluZaXi-tStKvmZIIHhbu8of-eJE");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<LightHub>("/lightHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();