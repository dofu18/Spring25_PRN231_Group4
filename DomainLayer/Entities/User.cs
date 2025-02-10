using System;
using System.Collections.Generic;
using DomainLayer.Entities;
namespace DomainLayer.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public float Credits { get; set; }
        public string Meta { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }

        // Navigation Properties
        public virtual User? Parent { get; set; }
    }
}