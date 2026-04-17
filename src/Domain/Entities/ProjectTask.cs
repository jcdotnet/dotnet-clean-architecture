using Domain.Enums;

namespace Domain.Entities;
public class ProjectTask
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public PriorityLevel Priority { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }

    public ProjectTask(string title, string description, PriorityLevel priority, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        CreatedAt = DateTime.UtcNow;
        IsCompleted = false;
    }

    public void MarkAsCompleted() => IsCompleted = true;
}
