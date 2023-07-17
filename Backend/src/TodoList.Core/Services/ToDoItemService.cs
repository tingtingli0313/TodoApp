using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Interfaces;

namespace TodoList.Core.Services;
public class ToDoItemService : IToDoItemService
{
    private readonly IToDoContext _dbContext;
    
    public ToDoItemService(IToDoContext toDoContext)
    {
        _dbContext = toDoContext;
    }

    public async Task<Result<TodoItem>> AddAsync(TodoItem newItem)
    {
        if(_dbContext.GetItems().Any(x => string.Equals(x.Description.ToLowerInvariant(), newItem.Description.ToLowerInvariant())))
        {
            return Result<TodoItem>.Conflict($"Description {newItem.Description} is already exist.");
        }

        _dbContext.GetItems().Add(newItem);
        await _dbContext.SaveChangesAsync();

        return new Result<TodoItem>(newItem);
    }

    public async Task<Result<List<TodoItem>>> GetAllItemsAsync()
    {
        var result = new Result<List<TodoItem>>(new List<TodoItem>());
        var items = await _dbContext.GetItems().ToListAsync();

        if (items.Any())
        {
            result = new Result<List<TodoItem>>(items);
        }

        return result;
    }

    public async Task<Result<TodoItem>> GetItemByIdAsyc(Guid id)
    {
        var item = await _dbContext.GetItems().FindAsync(id);

        if (item is null)
        {
            return Result<TodoItem>.NotFound();
        }

        return new Result<TodoItem>(item);
    }

    public async Task<Result<TodoItem>> UpdateAsync(TodoItem toUpdate)
    {
        var updateItem = await _dbContext.GetItems().FindAsync(toUpdate.Id);
        if (updateItem is null) return Result<TodoItem>.NotFound();

        updateItem.MarkComplete(toUpdate.IsCompleted);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return Result<TodoItem>.Error(new[] { ex.Message });
        }
       
        return new Result<TodoItem>(updateItem);
    }
}
