using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TodoList.Core;
using TodoList.Infrastructure.Data;

namespace TodoList.Api;

public static class SeedData
{
    public static readonly TodoItem ToDoItem1 = new TodoItem
    (
        "Try to get the sample to build."
    );
    public static readonly TodoItem ToDoItem2 = new TodoItem
    (
       "Review the different projects in the solution and how they relate to one another."
    );
    public static readonly TodoItem ToDoItem3 = new TodoItem
    (
       "Make sure all the tests run and review what they are doing."
    );

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new TodoContext(
            serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>()))
        {
            // Look for any TODO items.
            if (dbContext.ToDoItems.Any())
            {
                return;  
            }

            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(TodoContext dbContext)
    {
        foreach (var item in dbContext.ToDoItems)
        {
            dbContext.Remove(item);
        }
        dbContext.ToDoItems.Add(new Core.TodoItem(ToDoItem1.Description));
        dbContext.ToDoItems.Add(new Core.TodoItem(ToDoItem2.Description));
        dbContext.ToDoItems.Add(new Core.TodoItem(ToDoItem3.Description));

        dbContext.SaveChanges();
    }
}

