using Application.Common.Mappings;
using Application.Ports.Outbound;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks;
public class GetProjectTasksQueryHandler(IProjectTaskRepository repository, ProjectTaskMapper mapper) :
    IRequestHandler<GetProjectTasksQuery, Result<IEnumerable<ProjectTaskDto>>>
{   
    public async Task<Result<IEnumerable<ProjectTaskDto>>> Handle(GetProjectTasksQuery request, 
        CancellationToken cancellationToken)
    {

        var tasks = await repository.GetAsync(cancellationToken);

        return tasks.Select(mapper.ProjectTaskToDto).ToList();

    }
}
