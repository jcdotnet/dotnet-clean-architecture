using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        DbContext(options), IApplicationDbContext
    {
        public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();
    }
}
