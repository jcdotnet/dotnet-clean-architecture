using Domain.Entities;
using MediatR;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand, Guid>
    {
        public Task<Guid> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProjectTask(
                request.Title,
                request.Description,
                request.Priority,
                request.DueDate);

            // TO-DO later (when we have a DB)
            // _repository.Add(entity);
            // await _unitOfWork.SaveChangesAsync();

            return Task.FromResult(entity.Id);
        }
    }
}
