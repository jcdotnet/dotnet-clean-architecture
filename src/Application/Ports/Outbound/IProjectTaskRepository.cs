using Domain.Entities;

namespace Application.Ports.Outbound;

/// <summary>
/// Defines the outbound port required by the application to persist project tasks
/// Unlike the Clean Architecture project, this approach uses a repo instead of IApplicationDbContext
/// This adds an abstraction layer, but it keeps the use case independent of EF Core's API, and this
/// same approach, which is a design decision, could also be used in Clean Architecture
/// </summary>
public interface IProjectTaskRepository
{
    Task AddAsync(ProjectTask projectTask, CancellationToken cancellationToken);
}
