using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("target")]
    public class Target
    {
        [Column("target_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [MaxLength(1)]
        [Column("prefix")]
        public string Prefix { get; set; } = null!;

        [Column("group_link_id")]
        public Group? GroupLink { get; set; }
    }
}

