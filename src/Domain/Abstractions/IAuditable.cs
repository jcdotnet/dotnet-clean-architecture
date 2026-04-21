namespace Domain.Abstractions;

// Marks entities that need auto-generated timestamps to avoid manual work
public interface IAuditable
{
    DateTime CreatedAt { get; set; }
}
