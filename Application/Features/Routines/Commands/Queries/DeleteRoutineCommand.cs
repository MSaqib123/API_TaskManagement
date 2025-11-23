using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRoutine.Infrastructure.Repositories;

namespace Application.Features.Routines.Commands.Queries
{
    // DeleteRoutineCommand.cs
    public record DeleteRoutineCommand(Guid Id) : IRequest;

    // DeleteRoutineCommandHandler.cs
    public class DeleteRoutineCommandHandler(RoutineRepository repository) : IRequestHandler<DeleteRoutineCommand>
    {
        public async Task Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteRoutineAsync(request.Id);
        }
    }
}
