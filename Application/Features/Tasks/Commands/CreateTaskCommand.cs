using MediatR;
using System;

namespace TaskRoutine.Application.Features.Tasks.Commands;

public record CreateTaskCommand(
    string Title,
    string? Description,
    Priority Priority,
    string? Category,
    string? Status,
    string? Recurrence,
    DateTime? DueDate,
    int OrderIndex) : IRequest<Guid>;