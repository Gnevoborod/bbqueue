using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Database.Entities
{
    [Table("window_target")]
    internal sealed class WindowTargetEntity
    {
        [Key, Column("window_target_id")]
        public long Id { get; set; }

        [Column("window_id")]
        public long WindowId { get; set; }

        [ForeignKey(nameof(WindowId))]
        public WindowEntity Window { get; set; } = null!;

        [Column("target_id")]
        public long TargetId { get; set; }

        [ForeignKey(nameof(TargetId))]
        public TargetEntity Target { get; set; } = null!;
    }
}
