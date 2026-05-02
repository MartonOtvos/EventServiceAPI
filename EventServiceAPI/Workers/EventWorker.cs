using Microsoft.Extensions.Hosting;

public class EventWorker : BackgroundService
{
    private readonly IEventQueue _queue;
    private readonly ILogger<EventWorker> _logger;
    private readonly EventRepository _repo;

    public EventWorker(IEventQueue queue, ILogger<EventWorker> logger, EventRepository repo)
    {
        _queue = queue;
        _logger = logger;
        _repo = repo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Event worker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            var evt = await _queue.DequeueAsync(stoppingToken);

            // simulate processing
            _logger.LogInformation($"Processing event {evt.Type} from {evt.Source}");

            await Task.Delay(100, stoppingToken);

            await _repo.SaveAsync(evt);
        }
    }
}