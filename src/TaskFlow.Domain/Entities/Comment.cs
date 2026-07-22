using System;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public Guid TaskItemId { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Comment() { } // For EF Core

        public static Comment Create(Guid id, string content, Guid taskItemId, Guid authorId, DateTime createdAt)
        {
            if (string.IsNullOrEmpty(content))
                throw new DomainException("Content cannot be empty.");

            return new Comment
            {
                Id = id,
                Content = content,
                TaskItemId = taskItemId,
                AuthorId = authorId,
                CreatedAt = createdAt
            };
        }
    }
}