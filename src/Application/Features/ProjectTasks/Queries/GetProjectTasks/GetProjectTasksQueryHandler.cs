using Application.Common.Interfaces;
using Application.Common.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks;
public class GetProjectTasksQueryHandler(IApplicationDbContext context, ProjectTaskMapper mapper) :
    IRequestHandler<GetProjectTasksQuery, IEnumerable<ProjectTaskDto>>
{   
    public async Task<IEnumerable<ProjectTaskDto>> Handle(GetProjectTasksQuery request, 
        CancellationToken cancellationToken)
    {

        var tasks = await context.ProjectTasks
        .AsNoTracking()
        .ToListAsync(cancellationToken);

        return tasks.Select(mapper.ProjectTaskToDto);

    }
}
