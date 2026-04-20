using Microsoft.OpenApi;
using System.Text.Json.Serialization;

namespace Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            // Display Enums as strings in the JSON responses
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "JcDotNet Clean Architecture API", 
                Version = "v1" 
            });
        });

        return services;

    }
}