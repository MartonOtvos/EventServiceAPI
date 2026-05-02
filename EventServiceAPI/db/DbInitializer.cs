using Npgsql;


public class DbInitializer
{
    private readonly string _connectionString;

    public DbInitializer(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default") ?? "";
    }

    public async Task InitializeAsync()
    {
        if (string.IsNullOrEmpty(_connectionString))
            return;
        
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var path = Path.Combine(AppContext.BaseDirectory, "db", "events.sql");
        var sql = await File.ReadAllTextAsync(path);

        var cmd = new NpgsqlCommand(sql, conn);

        await cmd.ExecuteNonQueryAsync();
    }
}