using Application.Common.Behaviors;
using Application.Common.Mappings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            // Add Validation Behavior to MediatR pipeline
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Mapper
        services.AddSingleton<ProjectTaskMapper>();

        return services;
    }
}
