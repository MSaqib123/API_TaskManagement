using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRoutine.Infrastructure.Repositories;

namespace Application.Features.Tasks.Commands
{
    public record ClearCompletedCommand : IRequest;

    public class ClearCompletedCommandHandler(TaskRepository repository) : IRequestHandler<ClearCompletedCommand>
    {
        public async Task Handle(ClearCompletedCommand request, CancellationToken cancellationToken)
        {
            await repository.ClearCompletedAsync();
        }
    }
}
