using MediatR;
using TaskRoutine.Infrastructure.Repositories;

public record ToggleTaskCompleteCommand(Guid Id) : IRequest;

public class ToggleTaskCompleteCommandHandler(TaskRepository repository) : IRequestHandler<ToggleTaskCompleteCommand>
{
    public async Task Handle(ToggleTaskCompleteCommand request, CancellationToken cancellationToken)
    {
        await repository.ToggleCompleteAsync(request.Id);
    }
}