using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEventQueue, EventQueue>();
builder.Services.AddSingleton<EventRepository>();
builder.Services.AddSingleton<DbInitializer>();
builder.Services.AddHostedService<EventWorker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapPost("/events", async (EventDto dto, IEventQueue queue, ILogger<Program> logger) => 
{
    logger.LogInformation("Recieved event: {Type} from {Source}", dto.type, dto.source);   
    
    if (string.IsNullOrWhiteSpace(dto.Type))
        return Results.BadRequest("Type is required");

    await queue.EnqueueAsync(dto);
    return Results.Accepted();
});

app.MapGet("/events", async (EventRepository repo) =>
{
    logger.LogInformation("Recieved request for events");    
    var events = await repo.GetAllAsync();
    return Results.Ok(events);
});

app.MapGet("/health", () => Results.Ok("ok"));

app.Run();
