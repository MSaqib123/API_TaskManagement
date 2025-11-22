using Domain.Entities;
using MediatR;
using TaskRoutine.Infrastructure.Repositories;

namespace TaskRoutine.Application.Features.Tasks.Commands;

public class CreateTaskCommandHandler(TaskRepository repository) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Category = request.Category,
            Status = request.Status,
            Recurrence = request.Recurrence,
            DueDate = request.DueDate,
            OrderIndex = request.OrderIndex
        };
        return await repository.CreateTaskAsync(task);
    }
}