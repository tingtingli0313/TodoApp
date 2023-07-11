using Microsoft.EntityFrameworkCore;

namespace TodoList.Core.Interfaces
{
    public interface IToDoContext
    {
        DbSet<TodoItem> GetItems();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
