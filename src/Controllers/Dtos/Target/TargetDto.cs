namespace bbqueue.Controllers.Dtos.Target
{
    public sealed class TargetDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public char Prefix { get; set; }

        internal long? GroupLinkId { get; set; }
    }
}
