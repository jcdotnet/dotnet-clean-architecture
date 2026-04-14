using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandHandler(IApplicationDbContext context) : 
        IRequestHandler<CreateProjectTaskCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProjectTask(
                request.Title,
                request.Description,
                request.Priority,
                request.DueDate);

            context.ProjectTasks.Add(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
