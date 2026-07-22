using System;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; }
        public Priority Priority { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? AssignedToId { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public byte[] RowVersion { get; private set; }

        protected TaskItem() { } // For EF Core

        public static TaskItem Create(Guid id, string title, string description, TaskStatus status, Priority priority, Guid projectId, Guid? assignedToId, DateTime dueDate, DateTime createdAt)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException("Title cannot be empty.");

            return new TaskItem
            {
                Id = id,
                Title = title,
                Description = description,
                Status = status,
                Priority = priority,
                ProjectId = projectId,
                AssignedToId = assignedToId,
                DueDate = dueDate,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }

        public void Update(string title, string description, TaskStatus status, Priority priority, Guid? assignedToId, DateTime dueDate)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException("Title cannot be empty.");

            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            AssignedToId = assignedToId;
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}