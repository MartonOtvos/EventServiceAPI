using Npgsql;
using System.Data;

public class EventRepository
{
    private readonly string _connectionString;

    public EventRepository(IConfiguration config)
    {
        _connectionString = config.
            GetConnectionString("Default") ?? throw new Exception("Missing connection string");
    }

    public async Task SaveAsync(EventDto evt)
    {
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