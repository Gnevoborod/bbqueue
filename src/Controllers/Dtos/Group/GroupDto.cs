using bbqueue.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Group
{
    internal sealed class GroupDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public long? GroupLinkId { get; set; }

    }
}
