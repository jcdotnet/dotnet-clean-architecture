using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks
{
    public class GetProjectTasksQueryHandler(IApplicationDbContext context) :
        IRequestHandler<GetProjectTasksQuery, IEnumerable<ProjectTaskDto>>
    {
        public async Task<IEnumerable<ProjectTaskDto>> Handle(GetProjectTasksQuery request, 
            CancellationToken cancellationToken)
        {
            return await context.ProjectTasks
            .AsNoTracking()
            .Select(t => new ProjectTaskDto(
                t.Id,
                t.Title,
                t.Description,
                t.Priority,
                t.IsCompleted,
                t.DueDate))
            .ToListAsync(cancellationToken);

        }
    }
}
