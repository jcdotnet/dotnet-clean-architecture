using Domain.Abstractions;
using MediatR;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks;
public record GetProjectTasksQuery : IRequest<Result<IEnumerable<ProjectTaskDto>>>;
