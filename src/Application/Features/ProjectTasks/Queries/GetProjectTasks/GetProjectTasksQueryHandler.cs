using Application.Common.Interfaces;
using Application.Common.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProjectTasks.Queries.GetProjectTasks
{
    public class GetProjectTasksQueryHandler(IApplicationDbContext context) :
        IRequestHandler<GetProjectTasksQuery, IEnumerable<ProjectTaskDto>>
    {

        private readonly ProjectTaskMapper _mapper = new();
        
        public async Task<IEnumerable<ProjectTaskDto>> Handle(GetProjectTasksQuery request, 
            CancellationToken cancellationToken)
        {

            var tasks = await context.ProjectTasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return tasks.Select(_mapper.ProjectTaskToDto);

        }
    }
}
