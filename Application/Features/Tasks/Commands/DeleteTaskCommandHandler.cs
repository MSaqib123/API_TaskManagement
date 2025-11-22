using MediatR;
using TaskRoutine.Infrastructure.Repositories;

public record DeleteTaskCommand(Guid Id) : IRequest;

public class DeleteTaskCommandHandler(TaskRepository repository) : IRequestHandler<DeleteTaskCommand>
{
    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteTaskAsync(request.Id);
    }
}