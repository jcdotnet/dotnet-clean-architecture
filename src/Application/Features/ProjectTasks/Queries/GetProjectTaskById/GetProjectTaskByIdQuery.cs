using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using MediatR;

namespace Application.Features.ProjectTasks.Queries.GetProjectTaskById;
public record GetProjectTaskByIdQuery(Guid Id) : IRequest<ProjectTaskDto>;
