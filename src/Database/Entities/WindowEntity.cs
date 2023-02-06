using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Database.Entities
{
    [Table("window")]
    internal sealed class WindowEntity
    {
        [Key, Column("window_id")]
        public long Id { get; set; }

        [Column("number"), MaxLength(6)]
        public string Number { get; set; } = null!;

        [Column("description"), MaxLength(256)]
        public string? Description { get; set; }

        [Column("employee_id")]
        public long EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmployeeEntity? Employee { get; set; }
    }
}