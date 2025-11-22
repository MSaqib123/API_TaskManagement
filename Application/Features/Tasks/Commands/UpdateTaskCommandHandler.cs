using Domain.Entities;
using MediatR;
using TaskRoutine.Infrastructure.Repositories;

namespace TaskRoutine.Application.Features.Tasks.Commands;

public class UpdateTaskCommandHandler(TaskRepository repository) : IRequestHandler<UpdateTaskCommand>
{
    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Category = request.Category,
            Status = request.Status,
            Recurrence = request.Recurrence,
            DueDate = request.DueDate
        };
        await repository.UpdateTaskAsync(task);
    }
}