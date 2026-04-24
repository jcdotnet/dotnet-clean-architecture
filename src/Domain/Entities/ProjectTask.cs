using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities;
public class ProjectTask: IAuditable
{
    public Guid Id { get; private set; }

    public string Title
    {
        get;
        private set => field = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Title is required", nameof(Title)) : value.Trim();
    }

    public string Description
    {
        get;
        private set => field = value?.Trim() ?? string.Empty;
    }

    public PriorityLevel Priority { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; private set; }

    public ProjectTask(string title, string description, PriorityLevel priority, DateTime? dueDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        IsCompleted = false;
    }

    public void MarkAsCompleted() => IsCompleted = true;
}
