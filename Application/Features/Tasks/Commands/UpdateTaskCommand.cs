using MediatR;

namespace TaskRoutine.Application.Features.Tasks.Commands;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string? Description,
    Priority Priority,
    string? Category,
    string? Status,
    string? Recurrence,
    DateTime? DueDate) : IRequest;