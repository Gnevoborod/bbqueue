﻿using bbqueue.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Employee
{
    public class EmployeeDto
    {
        public long Id { get; set; }

        public string ExternalSystemIdentity { get; set; } = default!;

        public string? Name { get; set; }

        public bool Active { get; set; }

        public EmployeeRole Role { get; set; }
    }
}
