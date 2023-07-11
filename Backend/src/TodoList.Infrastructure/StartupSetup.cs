using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Core.Interfaces;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure;
public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TodoContext>(options =>
               options.UseInMemoryDatabase(connectionString));

        services.AddScoped<IToDoContext, TodoContext>();
    }
}
