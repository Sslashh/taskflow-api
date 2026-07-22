using System;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Project() { } // For EF Core

        public static Project Create(Guid id, string name, string description, Guid ownerId, DateTime createdAt)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Name cannot be empty.");

            return new Project
            {
                Id = id,
                Name = name,
                Description = description,
                OwnerId = ownerId,
                CreatedAt = createdAt
            };
        }
    }
}