﻿using EmployeeService.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Database.Entities
{
    [Table("employee")]
    public sealed class EmployeeEntity
    {
        [Key, Column("employee_id")]
        public long Id { get; set; }

        [Column("external_system_id"), MaxLength(16)]
        public string ExternalSystemIdentity { get; set; } = default!;

        [Column("name"), MaxLength(100)]
        public string? Name { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("role")]
        public EmployeeRole Role { get; set; }

    }
}