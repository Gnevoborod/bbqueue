using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("window_target")]
    public class WindowTarget
    {
        [Column("window_target_id")]
        public int Id { get; set; }

        [Column("window_id")]
        public Window Window { get; set; } = null!;

        [Column("target_id")]
        public Target Target { get; set; } = null!;
    }
}
