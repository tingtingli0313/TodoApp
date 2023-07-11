using Ardalis.GuardClauses;
using System.Xml.Linq;
using TodoList.Core.Shared;
using TodoList.Core.Shared.Interfaces;

namespace TodoList.Core;

public class TodoItem : EntityBase, IAggregateRoot
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