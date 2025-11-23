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
    public class GetDueRoutinesQueryHandler(RoutineRepository repository) : IRequestHandler<GetDueRoutinesQuery, IEnumerable<RoutineItem>>
    {
        public async Task<IEnumerable<RoutineItem>> Handle(GetDueRoutinesQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetDueRoutinesAsync(request.CurrentTime);
        }
    }
}
