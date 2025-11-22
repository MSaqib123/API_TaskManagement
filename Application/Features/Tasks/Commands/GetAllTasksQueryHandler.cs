using Domain.Entities;
using MediatR;
using TaskRoutine.Infrastructure.Repositories;

namespace TaskRoutine.Application.Features.Tasks.Queries;

public class GetAllTasksQueryHandler(TaskRepository repository) : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItem>>
{
    public async Task<IEnumerable<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllTasksAsync(request.Search, request.Category, request.Status, request.Priority, request.Recurrence);
    }
}