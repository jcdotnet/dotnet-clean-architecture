using Domain.Enums;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks
{
    public record ProjectTaskDto(
    Guid Id,
    string Title,
    string Description,
    PriorityLevel Priority,
    bool IsCompleted,
    DateTime? DueDate);

}
