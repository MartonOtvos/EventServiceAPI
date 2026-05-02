using Npgsql;
using System.Data;

public class EventRepository
{
    private readonly string _connectionString;
    private List<EventDto> _fallbackList;

    public EventRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default") ?? "";
        _fallbackList = new List<EventDto>();

        if (string.IsNullOrEmpty(_connectionString))
        {
            Console.WriteLine("!!! NO CONNECTION STRING: USING IN MEMORY LIST");
        }
    }

    public async Task SaveAsync(EventDto evt)
    {
        if (string.IsNullOrEmpty(_connectionString))
        {
            _fallbackList.Add(evt);
            return;
        }

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand(
            "INSERT INTO events(type, source, created_at) VALUES (@type, @source, NOW())",
            conn
        );

        cmd.Parameters.AddWithValue("type", evt.Type);
        cmd.Parameters.AddWithValue("source", evt.Source);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<EventDto>> GetAllAsync()
    {
        if (string.IsNullOrEmpty(_connectionString))
        {
            return _fallbackList;
        }

        var result = new List<EventDto>();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand(
            "SELECT type, source FROM events",
            conn
        );

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new EventDto(
                reader["type"] as string ?? "",
                reader["source"] as string ?? "",
                new PayloadDto("placeholder")
            ));
        }

        return result;
    }
}