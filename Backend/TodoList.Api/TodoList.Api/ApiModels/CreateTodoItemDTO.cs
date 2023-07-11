using System;

namespace TodoList.Api.ApiModels;

public class TodoItemDTO : CreateTodoItemDTO
{
    public TodoItemDTO(Guid id, string description) : base(description)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class CreateTodoItemDTO
{
    public CreateTodoItemDTO() { }

    protected CreateTodoItemDTO(string description)
    {
        Description = description;
    }

    public string Description { get; set; }
}


public class UpdateTodoItemDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}