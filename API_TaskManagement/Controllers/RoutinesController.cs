using Application.Features.Routines.Commands.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskRoutine.Api.Controllers;

[ApiController]
[Route("api/routines")]
public class RoutinesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRoutineCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteRoutineCommand(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoutineItem>>> GetAll()
    {
        var routines = await mediator.Send(new GetAllRoutinesQuery());
        return Ok(routines);
    }

    [HttpGet("due")]
    public async Task<ActionResult<IEnumerable<RoutineItem>>> GetDue([FromQuery] string time)
    {
        var routines = await mediator.Send(new GetDueRoutinesQuery(time));
        return Ok(routines);
    }
}