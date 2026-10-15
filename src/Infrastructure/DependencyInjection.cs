using Application.Ports.Outbound;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureServices(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<AuditableEntityInterceptor>();
                options.UseSqlServer(connectionString).AddInterceptors(interceptor);
            });

            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();

            return services;
        }
    }
}
