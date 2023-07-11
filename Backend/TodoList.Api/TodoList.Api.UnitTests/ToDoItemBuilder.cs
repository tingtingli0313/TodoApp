using System;
using TodoList.Core;

namespace Clean.Architecture.UnitTests;

// Learn more about test builders:
// https://ardalis.com/improve-tests-with-the-builder-pattern-for-test-data
public class ToDoItemBuilder
{
    private TodoItem _todo = new TodoItem("test");

    public ToDoItemBuilder Id(Guid id)
    {
        _todo.Id = id;
        return this;
    }

    public ToDoItemBuilder Description(string description)
    {
        _todo.Description = description;
        return this;
    }

    public ToDoItemBuilder WithDefaultValues()
    {
        _todo = new TodoItem("Test Item") { Id = new Guid(), Description = "Test Description" };

        return this;
    }

    public TodoItem Build() => _todo;
}
