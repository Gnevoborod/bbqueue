using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("employee")]
    public class Employee
    {
        [Column("employee_id")]
        public int Id { get; set; }

        [Column("external_system_id")]
        public string ExternalSystemIdentity { get; set; } = null!;

        [Column("name")]
        public string? Name { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}