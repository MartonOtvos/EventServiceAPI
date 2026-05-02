public record PayloadDto(string Status);

public record EventDto(
    string Type,
    string Source,
    PayloadDto Payload
);

public class EventEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = "";
    public string Source { get; set; } = "";
    public string Status { get; set; } = "pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

