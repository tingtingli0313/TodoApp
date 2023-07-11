using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace TodoList.Core.Shared;

public abstract class EntityBase
{
    public Guid Id { get; set; }
}