using Npgsql;


public class DbInitializer
{
    private readonly string _connectionString;

    public DbInitializer(IConfiguration config)
    {
        _connectionString = config
            .GetConnectionString("Default") ?? throw new Exception("Missing connection string");
    }

    public async Task InitializeAsync()
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var path = Path.Combine(AppContext.BaseDirectory, "schema", "events.sql");
        var sql = await File.ReadAllTextAsync(path);

        var cmd = new NpgsqlCommand(sql, conn);

        await cmd.ExecuteNonQueryAsync();
    }
}