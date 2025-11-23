using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRoutine.Infrastructure.Repositories;

namespace Application.Features.Routines.Commands.Queries
{
    public class CreateRoutineCommandHandler(RoutineRepository repository) : IRequestHandler<CreateRoutineCommand, Guid>
    {
        public async Task<Guid> Handle(CreateRoutineCommand request, CancellationToken cancellationToken)
        {
            var routine = new RoutineItem
            {
                Title = request.Title,
                Notes = request.Notes,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };
            return await repository.CreateRoutineAsync(routine);
        }
    }
}
