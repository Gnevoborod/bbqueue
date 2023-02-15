namespace bbqueue.Controllers.Dtos.Target
{
    internal sealed class TargetDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public char Prefix { get; set; }

        public long? GroupLinkId { get; set; }
    }
}
