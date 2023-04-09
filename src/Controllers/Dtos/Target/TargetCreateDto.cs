using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Target
{
    public class TargetCreateDto
    {
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        [MaxLength(256)]
        public string? Description { get; set; }

        public char Prefix { get; set; }

        public long? GroupLinkId { get; set; }
    }
}
