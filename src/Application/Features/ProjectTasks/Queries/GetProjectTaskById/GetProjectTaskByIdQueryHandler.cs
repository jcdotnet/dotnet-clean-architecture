using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using Application.Ports.Outbound;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;

namespace Application.Features.ProjectTasks.Queries.GetProjectTaskById;
public class GetProjectTaskByIdQueryHandler(IProjectTaskRepository repository, ProjectTaskMapper mapper) :
    IRequestHandler<GetProjectTaskByIdQuery, Result<ProjectTaskDto>>
{
    public async Task<Result<ProjectTaskDto>> Handle(GetProjectTaskByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            return Result.FailureResult<ProjectTaskDto>(ProjectTaskErrors.NotFound(request.Id));

        return mapper.ProjectTaskToDto(entity);
    }
}