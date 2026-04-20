using Api.Infrastructure;
using Application;
using Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    Log.Information("Starting JcDotNet Clean Architecture API...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddPresentationServices();

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