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

        return await (result switch
        {
            ProjectTask projectTask => SaveAsync(projectTask),
            Error error => Task.FromResult<Result<Guid>>(error),
            null => Task.FromResult<Result<Guid>>(Error.NullValue)
        });

        async Task<Result<Guid>> SaveAsync(ProjectTask projectTask)
        {
            context.ProjectTasks.Add(projectTask);
            await context.SaveChangesAsync(cancellationToken);

            return projectTask.Id;
        }
    }
}
