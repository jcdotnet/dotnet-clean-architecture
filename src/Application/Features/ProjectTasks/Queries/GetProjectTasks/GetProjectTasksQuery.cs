using MediatR;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks
{
    public record GetProjectTasksQuery : IRequest<IEnumerable<ProjectTaskDto>>;
}
