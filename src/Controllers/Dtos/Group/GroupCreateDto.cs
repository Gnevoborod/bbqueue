namespace bbqueue.Controllers.Dtos.Group
{
    public class GroupCreateDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public long? GroupLinkId { get; set; }
    }
}
