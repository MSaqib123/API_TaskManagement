// Step 8: API Layer
// src/TaskRoutine.Api/Controllers/TasksController.cs
using Application.Features.Tasks.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskRoutine.Application.Features.Tasks.Commands;
using TaskRoutine.Application.Features.Tasks.Queries;

namespace TaskRoutine.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTaskCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
    {
        command = command with { Id = id };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/toggle")]
    public async Task<ActionResult> ToggleComplete(Guid id)
    {
        await mediator.Send(new ToggleTaskCompleteCommand(id));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteTaskCommand(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] string? status,
        [FromQuery] int? priority,
        [FromQuery] string? recurrence)
    {
        var query = new GetAllTasksQuery(search, category, status, priority, recurrence);
        var tasks = await mediator.Send(query);
        return Ok(tasks);
    }

    [HttpPut("order")]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateTaskOrderCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("completed")]
    public async Task<ActionResult> ClearCompleted()
    {
        await mediator.Send(new ClearCompletedCommand());
        return NoContent();
    }
}