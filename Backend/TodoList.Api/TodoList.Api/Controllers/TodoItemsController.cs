using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.ApiModels;
using TodoList.Core;
using TodoList.Core.Interfaces;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IToDoItemService _todoService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(IToDoItemService context, ILogger<TodoItemsController> logger)
        {
            _todoService = context;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var results = (await _todoService.GetAllItemsAsync()).Value.OrderBy(x=> x.IsCompleted);
            return Ok(results);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var result = await _todoService.GetItemByIdAsyc(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

        //PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, UpdateTodoItemDTO todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var updateItem = new TodoItem(todoItem.Description);
            updateItem.Id = id;
            updateItem.MarkComplete(todoItem.IsCompleted);
            await _todoService.UpdateAsync(updateItem);
            return NoContent();
        }

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] CreateTodoItemDTO request)
        {
            var newItem = new TodoItem(request.Description);

            var createdTodoItem = await _todoService.AddAsync(newItem);
            if (!createdTodoItem.IsSuccess)
            {
                return BadRequest(createdTodoItem.Errors.First());
            }

            var result = new TodoItemDTO
            (
                id: createdTodoItem.Value.Id,
                description: createdTodoItem.Value.Description
            );
            return CreatedAtAction(nameof(GetTodoItem), new { id = result.Id }, result);
        }
    }
}
