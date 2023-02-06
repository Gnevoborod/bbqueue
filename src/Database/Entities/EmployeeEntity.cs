using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bbqueue.Database.Entities
{
    [Table("employee")]
    internal sealed class EmployeeEntity
    {
        [Key, Column("employee_id")]
        public long Id { get; set; }

        [Column("external_system_id"), MaxLength(16)]
        public string ExternalSystemIdentity { get; set; } = null!;

        [Column("name"), MaxLength(100)]
        public string? Name { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}