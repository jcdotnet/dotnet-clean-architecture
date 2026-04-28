using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProjectTasks.Queries.GetProjectTaskById;
public class GetProjectTaskByIdQueryHandler(IApplicationDbContext context, ProjectTaskMapper mapper) :
    IRequestHandler<GetProjectTaskByIdQuery, Result<ProjectTaskDto>>
{
    public async Task<Result<ProjectTaskDto>> Handle(GetProjectTaskByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await context.ProjectTasks
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result.FailureResult<ProjectTaskDto>(ProjectTaskErrors.NotFound(request.Id));

        return mapper.ProjectTaskToDto(entity);
    }
}