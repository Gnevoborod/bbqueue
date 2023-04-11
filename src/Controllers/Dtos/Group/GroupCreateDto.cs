using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Group
{
    public class GroupCreateDto
    {
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        [MaxLength(256)]
        public string? Description { get; set; }

        public long? GroupLinkId { get; set; }
    }
}
