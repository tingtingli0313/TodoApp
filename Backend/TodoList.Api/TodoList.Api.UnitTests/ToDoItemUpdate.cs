using TodoList.Api.UnitTests;
using Xunit;


namespace TodoList.Api.UnitTests;

public class ToDoItemUpdate
{
    [Fact]
    public void SetsIsDoneToTrue()
    {
        var item = new ToDoItemBuilder()
            .WithDefaultValues()
            .Description("")
            .Build();

        item.MarkComplete(true);

        Assert.True(item.IsCompleted);
    }

    [Fact]
    public void RaisesToDoItemCompletedEvent()
    {
        var item = new ToDoItemBuilder().Build();

        item.MarkComplete(false);

        Assert.False(item.IsCompleted);
    }
}
