using Application.Ports.Outbound;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProjectTaskRepository(ApplicationDbContext context) : IProjectTaskRepository
{
    public async Task AddAsync(ProjectTask projectTask, CancellationToken cancellationToken)
    {
        context.ProjectTasks.Add(projectTask);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProjectTask>> GetAsync(CancellationToken cancellationToken)
    {
        return await context.ProjectTasks.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.ProjectTasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
