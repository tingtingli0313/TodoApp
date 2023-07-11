using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TodoList.Infrastructure.Data;

namespace IntegrationTest
{
    public abstract class BaseEfRepoTestFixture
    {
        protected TodoContext _dbContext;

        protected BaseEfRepoTestFixture()
        {
            var options = CreateNewContextOptions();

            _dbContext = new TodoContext(options);
        }

        protected static DbContextOptions<TodoContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<TodoContext>();
            builder.UseInMemoryDatabase("TodoItems")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

    }

}