using Domain.Entities;
using MediatR;

namespace TaskRoutine.Application.Features.Tasks.Queries;

public record GetAllTasksQuery(
    string? Search = null,
    string? Category = null,
    string? Status = null,
    int? Priority = null,
    string? Recurrence = null) : IRequest<IEnumerable<TaskItem>>;