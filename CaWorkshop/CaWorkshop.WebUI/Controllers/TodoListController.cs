using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Application.TodoLists.Commands.DeleteTodoList;
using CaWorkshop.Application.TodoLists.Commands.UpdateTodoList;
using CaWorkshop.Application.TodoLists.Queries.GetTodoLists;
using CaWorkshop.Domain.Entities;
using CaWorkshop.Infrastructure.Data;
using CaWorkshop.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilterAttribute]
    public class TodoListController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TodoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/TodoList
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        //{
        //  if (_context.TodoLists == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.TodoLists.ToListAsync();
        //}


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists([FromServices] IGetTodoListsQuery query)
        //{
        //    return await query.Handle();
        //}

        // GET: api/TodoLists
        [HttpGet]
        public async Task<ActionResult<TodosVm>> GetTodoLists()
        {
            return await _mediator.Send(new GetTodoListsQuery());
        }

        // POST: api/TodoLists
        [HttpPost]
        public async Task<ActionResult<int>> PostTodoList(
            CreateTodoListCommand command)
        {
            return await _mediator.Send(command);
        }

        // PUT: api/TodoLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id,
            UpdateTodoListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/TodoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            await _mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}
