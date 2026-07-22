using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(
    string Title,
    string? Description,
    Priority Priority,
    Guid ProjectId,
    Guid? AssignedToId,
    DateTime? DueDate) : IRequest<Result<Guid>>;
