using Application.Ports.Outbound;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask;
public class CreateProjectTaskCommandHandler(IProjectTaskRepository repository) : 
    IRequestHandler<CreateProjectTaskCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var result = ProjectTask.Create(
            request.Title,
            request.Description,
            request.Priority,
            request.DueDate);

        if (!result.IsSuccess)
        {
            return Result.FailureResult<Guid>(result.Error);
        }

        await repository.AddAsync(result.Value!, cancellationToken);

        return result.Value!.Id;
    }
}
