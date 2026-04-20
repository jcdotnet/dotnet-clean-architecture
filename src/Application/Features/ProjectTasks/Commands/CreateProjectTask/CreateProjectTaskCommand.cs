using Domain.Enums;
using MediatR;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask;

// The Command: What data do we need to create a task?
public record CreateProjectTaskCommand(
    string Title,
    string Description,
    PriorityLevel Priority,
    DateTime? DueDate) : IRequest<Guid>;
