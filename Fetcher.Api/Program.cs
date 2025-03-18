using Fetcher.Api;
using Serilog;

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    logger.Information("Starting application");
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureApiInfra();

    var app = builder.Build();
    app.UseApiInfra();

    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Application start-up failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}