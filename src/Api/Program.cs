using Api.Infrastructure;
using Application;
using Infrastructure;
using Serilog;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    Log.Information("Starting JcDotNet Clean Architecture API...");

    var builder = WebApplication.CreateBuilder(args);

    // Add serilog as the logging provider
    builder.Host.UseSerilog((context, config) => config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    // Add services to the container.

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Display Enums as strings in the JSON responses
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config =>
    {
        config.SwaggerDoc("v1", new() { Title = "JcDotNet Clean Architecture API", Version = "v1" });
    });

    var app = builder.Build();

    app.UseExceptionHandler();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/v1/swagger.json", "JcDotNet API V1");
            config.RoutePrefix = string.Empty; // This makes Swagger the home page
        });
    }

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start");
}
finally
{
    Log.CloseAndFlush();
}