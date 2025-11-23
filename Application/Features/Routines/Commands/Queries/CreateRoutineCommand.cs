using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Routines.Commands.Queries
{
    public record CreateRoutineCommand(
    string Title,
    string? Notes,
    string? StartTime,
    string? EndTime) : IRequest<Guid>;
}
