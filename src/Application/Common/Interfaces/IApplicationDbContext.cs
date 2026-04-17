using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<ProjectTask> ProjectTasks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
