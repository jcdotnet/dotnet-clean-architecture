using Application.Common.Interfaces;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask;
public class CreateProjectTaskCommandHandler(IApplicationDbContext context) : 
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

        context.ProjectTasks.Add(result.Value!);
        await context.SaveChangesAsync(cancellationToken);

        return result.Value!.Id;
    }
}
