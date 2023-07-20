using Ardalis.GuardClauses;
using TodoList.Core.Shared;

namespace TodoList.Core;

public class TodoItem : EntityBase
{
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; private set; }

    public void MarkComplete(bool isCompleted)
    {
       IsCompleted = isCompleted;
    }

    public TodoItem(string description)
    {
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
    }

    public override string ToString()
    {
        string status = IsCompleted ? "Completed!" : "Not Completed.";
        return $"{Id}: Status: {status} - {Description}";
    }
}