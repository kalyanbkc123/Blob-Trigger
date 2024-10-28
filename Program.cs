using Azure_Blob_Trigger;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {   

        // Add Application Insights for monitoring
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Add EF Core DbContext with Sql Server
        services.AddDbContext<StudentContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString")));

        // Register other service if needed
        services.AddScoped<AzureBlobTriggerFunction>(); // Register your function class


        services.AddLogging();
    })
    .Build();

host.Run();