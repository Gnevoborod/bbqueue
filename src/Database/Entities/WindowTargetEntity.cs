using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Database.Entities
{
    [Table("window_target")]
    public sealed class WindowTargetEntity
    {
        [Key, Column("window_target_id")]
        public long Id { get; set; }

        [Column("window_id")]
        public long WindowId { get; set; }

        [ForeignKey(nameof(WindowId))]
        public WindowEntity Window { get; set; } = default!;

        [Column("target_id")]
        public long TargetId { get; set; }

        [ForeignKey(nameof(TargetId))]
        internal TargetEntity Target { get; set; } = default!;
    }
}
