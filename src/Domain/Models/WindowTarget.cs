namespace bbqueue.Domain.Models
{
    internal sealed class WindowTarget
    {
        public long Id { get; set; }
        public long WindowId { get; set; }
        public Window Window { get; set; } = null!;
        public long TargetId { get; set; }
        public Target Target { get; set; } = null!;
    }
}
