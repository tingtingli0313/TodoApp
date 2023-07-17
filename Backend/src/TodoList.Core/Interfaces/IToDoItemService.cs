using Ardalis.Result;
namespace TodoList.Core.Interfaces;

public interface IToDoItemService
{
    Task<Result<TodoItem>> AddAsync(TodoItem newItem);
    Task<Result<List<TodoItem>>> GetAllItemsAsync();
    Task<Result<TodoItem>> GetItemByIdAsyc(Guid id);
    Task<Result<TodoItem>> UpdateAsync(TodoItem value);
}