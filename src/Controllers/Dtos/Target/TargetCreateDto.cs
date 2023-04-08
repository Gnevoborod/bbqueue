namespace bbqueue.Controllers.Dtos.Target
{
    public class TargetCreateDto
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public char Prefix { get; set; }

        public long? GroupLinkId { get; set; }
    }
}
