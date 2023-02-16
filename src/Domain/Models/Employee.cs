﻿using System.ComponentModel.DataAnnotations;

namespace bbqueue.Domain.Models
{
    public enum EmployeeRole { Employee, Manager};
    internal sealed class Employee
    {
        public long Id { get; set; }

        [MaxLength(16)]
        public string ExternalSystemIdentity { get; set; } = null!;

        [MaxLength(100)]
        public string? Name { get; set; }

        public bool Active { get; set; }

        public EmployeeRole Role { get; set; }
    }
}