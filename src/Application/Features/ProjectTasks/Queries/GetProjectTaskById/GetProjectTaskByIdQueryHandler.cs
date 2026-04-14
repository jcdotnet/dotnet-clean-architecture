using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProjectTasks.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdQueryHandler(IApplicationDbContext context) :
        IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskDto>
    {
        
        private readonly ProjectTaskMapper _mapper = new();

        public async Task<ProjectTaskDto> Handle(GetProjectTaskByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var entity = await context.ProjectTasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new KeyNotFoundException($"Task with ID {request.Id} was not found.");

            return _mapper.ProjectTaskToDto(entity);
        }
    }
}
