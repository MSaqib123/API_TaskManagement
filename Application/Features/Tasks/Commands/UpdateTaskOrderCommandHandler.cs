using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRoutine.Infrastructure.Repositories;

namespace Application.Features.Tasks.Commands
{
    public record UpdateTaskOrderCommand(Guid Id, int NewIndex) : IRequest;

    public class UpdateTaskOrderCommandHandler(TaskRepository repository) : IRequestHandler<UpdateTaskOrderCommand>
    {
        public async Task Handle(UpdateTaskOrderCommand request, CancellationToken cancellationToken)
        {
            await repository.UpdateOrderAsync(request.Id, request.NewIndex);
        }
    }
}
