using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaWorkshop.Infrastructure.Data;
using CaWorkshop.Domain.Entities;
using MediatR;
using CaWorkshop.Application.TodoItems.Commands.CreateTodoItem;
using CaWorkshop.Application.TodoItems.Commands.DeleteTodoItem;
using CaWorkshop.Application.TodoItems.Commands.UpdateTodoItem;
using CaWorkshop.WebUI.Filters;

namespace CaWorkshop.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilterAttribute]
    public class TodoItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //private readonly ApplicationDbContext _context;

        //public TodoItemController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/TodoItem
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        //{
        //  if (_context.TodoItems == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.TodoItems.ToListAsync();
        //}

        //// GET: api/TodoItem/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        //{
        //  if (_context.TodoItems == null)
        //  {
        //      return NotFound();
        //  }
        //    var todoItem = await _context.TodoItems.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return todoItem;
        //}

        //// PUT: api/TodoItem/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        //{
        //    if (id != todoItem.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(todoItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TodoItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/TodoItem
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<int>> PostTodoItem(TodoItem todoItem)
        //{
        //    if (_context.TodoItems == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.TodoItems'  is null.");
        //    }
        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return todoItem.Id;

        //    //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        //}

        //// DELETE: api/TodoItem/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(int id)
        //{
        //    if (_context.TodoItems == null)
        //    {
        //        return NotFound();
        //    }
        //    var todoItem = await _context.TodoItems.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TodoItems.Remove(todoItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TodoItemExists(int id)
        //{
        //    return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<int>> PostTodoItem(
            CreateTodoItemCommand command)
        {
            return await _mediator.Send(command);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id,
            UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}
