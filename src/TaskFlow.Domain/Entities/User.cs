using System;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Role Role { get; private set; }

        protected User() { } // For EF Core

        public static User Create(Guid id, string email, string passwordHash, string firstName, string lastName, DateTime createdAt, Role role)
        {
            if (string.IsNullOrEmpty(email))
                throw new DomainException("Email cannot be empty.");
            if (!email.Contains("@"))
                throw new DomainException("Invalid email format.");

            return new User
            {
                Id = id,
                Email = email,
                PasswordHash = passwordHash,
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = createdAt,
                Role = role
            };
        }
    }
}