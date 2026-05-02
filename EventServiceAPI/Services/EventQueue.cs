using System.Threading.Channels;

public class EventQueue : IEventQueue
{
    private readonly Channel<EventDto> _queue;

    public EventQueue()
    {
        _queue = Channel.CreateUnbounded<EventDto>();
    }

    public async ValueTask EnqueueAsync(EventDto evt)
    {
        await _queue.Writer.WriteAsync(evt);
    }

    public async ValueTask<EventDto> DequeueAsync(CancellationToken ct)
    {
        return await _queue.Reader.ReadAsync(ct);
    }
}