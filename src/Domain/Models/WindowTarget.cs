namespace bbqueue.Domain.Models
{
    public sealed class WindowTarget
    {
        public long Id { get; set; }
        public long WindowId { get; set; }
        public Window Window { get; set; } = default!;
        public long TargetId { get; set; }
        public Target Target { get; set; } = default!;
    }
}
