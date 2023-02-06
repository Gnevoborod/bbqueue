using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bbqueue.Database.Entities
{
    [Table("group")]
    internal sealed class GroupEntity
    {
        [Key, Column("group_id")]
        public long Id { get; set; }

        [Column("name"), MaxLength(64)]
        public string Name { get; set; } = null!;

        [Column("description"), MaxLength(256)]
        public string? Description { get; set; }

        [Column("group_link_id")]
        public long? GroupLinkId { get; set; }

        [ForeignKey(nameof(GroupLinkId))]
        public GroupEntity? GroupLink { get; set; }
    }
}
