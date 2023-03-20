using System;
using CaWorkshop.Application.Common.Exceptions;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace CaWorkshop.Application.TodoLists.Commands.DeleteTodoList;


public class DeleteTodoListCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoListCommandHandler
    : AsyncRequestHandler<DeleteTodoListCommand>
{
    private readonly IApplicationDbContext _context​;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(DeleteTodoListCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }
        _context.TodoLists.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

