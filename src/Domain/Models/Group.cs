﻿using System.ComponentModel.DataAnnotations;


namespace bbqueue.Domain.Models
{
    public sealed class Group
    {
        public long Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; } = default!;

        [MaxLength(256)]
        public string? Description { get; set; }        
        public long? GroupLinkId { get; set; }
        public Group? GroupLink { get; set; }
    }
}
