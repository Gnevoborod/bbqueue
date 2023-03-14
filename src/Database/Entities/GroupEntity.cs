using bbqueue.Controllers.Dtos.Group;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bbqueue.Database.Entities
{
    [Table("group")]
    internal sealed class GroupEntity
    {
        [Key, Column("group_id")]
        public long Id { get; set; }

        [Column("name"), MaxLength(64)]
        public string Name { get; set; } = default!;

        [Column("description"), MaxLength(256)]
        public string? Description { get; set; }

        [Column("group_link_id")]
        public long? GroupLinkId { get; set; }

        [ForeignKey(nameof(GroupLinkId))]
        public GroupEntity? GroupLink { get; set; } = default!;
    }
}
