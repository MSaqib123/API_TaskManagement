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
    // GetAllRoutinesQuery.cs
    public record GetAllRoutinesQuery : IRequest<IEnumerable<RoutineItem>>;

    // GetAllRoutinesQueryHandler.cs
    public class GetAllRoutinesQueryHandler(RoutineRepository repository) : IRequestHandler<GetAllRoutinesQuery, IEnumerable<RoutineItem>>
    {
        public async Task<IEnumerable<RoutineItem>> Handle(GetAllRoutinesQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllRoutinesAsync();
        }
    }
}
