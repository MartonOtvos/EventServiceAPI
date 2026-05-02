public interface IEventQueue
{
    ValueTask EnqueueAsync(EventDto evt);
    ValueTask<EventDto> DequeueAsync(CancellationToken ct);
}