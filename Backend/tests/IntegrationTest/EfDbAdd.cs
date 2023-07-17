using Microsoft.EntityFrameworkCore;
using TodoList.Core;
namespace IntegrationTest;

public class EfDbAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddsProjectAndSetsId()
    {
        var testItemDesc = "testItem";
        var todoItem = new TodoItem(testItemDesc);

        await _dbContext.AddAsync(todoItem);
        await _dbContext.SaveChangesAsync();

        var result = (_dbContext.GetItems())
                        .FirstOrDefault();

        Assert.Equal(testItemDesc, result?.Description);
        Assert.NotNull(result?.Id);
    }


    [Fact]
    public async Task UpdatesItemAfterAddingIt()
    {
        // add a item
        var initialName = Guid.NewGuid().ToString();
        var testItemDesc = "testItem";
        var todoItem = new TodoItem(testItemDesc);

        await _dbContext.AddAsync(todoItem);
        _dbContext.SaveChanges();
        // detach the item so we get a different instance
        _dbContext.Entry(todoItem).State = EntityState.Detached;
        
        // fetch the item and update its title
        var newTodoItem = (_dbContext.GetItems())
            .FirstOrDefault(item => item.Description == testItemDesc);
        if (newTodoItem == null)
        {
            Assert.NotNull(newTodoItem);
            return;
        }
        Assert.NotSame(todoItem, newTodoItem);
        newTodoItem.MarkComplete(true);

        // Update the item
        _dbContext.Update(newTodoItem);
        _dbContext.SaveChanges();

        // Fetch the updated item
        var updatedItem = _dbContext.GetItems()
            .FirstOrDefault(project => project.Description == testItemDesc);

        Assert.NotNull(updatedItem);
        Assert.Equal(todoItem.Description, updatedItem?.Description);
        Assert.Equal(todoItem.Id, updatedItem?.Id);
        Assert.Equal(true, updatedItem?.IsCompleted);
    }
}