using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.FunctionTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Development"); // will not send real emails
        var host = builder.Build();
        host.Start();

        // Get service provider.
        var serviceProvider = host.Services;

        // Create a scope to obtain a reference to the database
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TodoContext>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            // Ensure the database is created.
            db.Database.EnsureCreated();

            try
            {
                // Can also skip creating the items
                //if (!db.ToDoItems.Any())
                //{
                // Seed the database with test data.
                SeedData.PopulateTestData(db);
                //}
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {exceptionMessage}", ex.Message);
            }
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(DbContextOptions<TodoContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // This should be set for each individual test run
                string inMemoryCollectionName = Guid.NewGuid().ToString();

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<TodoContext>(options =>
                {
                    options.UseInMemoryDatabase(inMemoryCollectionName);
                });
            });
    }
}
