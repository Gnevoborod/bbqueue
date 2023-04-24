using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using bbqueue.Domain.Models;

namespace bbqueue.Database.Entities
{
    [Table("window")]
    public sealed class WindowEntity
    {
        [Key, Column("window_id")]
        public long Id { get; set; }

        [Column("number"), MaxLength(6)]
        public string Number { get; set; } = default!;

        [Column("description"), MaxLength(256)]
        public string? Description { get; set; }

        [Column("employee_id")]
        public long? EmployeeId { get; set; }

        [Column("window_work_state")]
        public WindowWorkState WindowWorkState { get; set; }
    }
}