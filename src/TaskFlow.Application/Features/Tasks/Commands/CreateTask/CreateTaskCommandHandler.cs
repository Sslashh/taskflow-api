using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>()
            .GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result<Guid>.Failure($"Project with id '{request.ProjectId}' was not found.");

        var taskItem = TaskItem.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description ?? string.Empty,
            TaskItemStatus.Todo,
            request.Priority,
            request.ProjectId,
            request.AssignedToId,
            request.DueDate,
            DateTime.UtcNow);

        await _unitOfWork.Repository<TaskItem>().AddAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(taskItem.Id);
    }
}