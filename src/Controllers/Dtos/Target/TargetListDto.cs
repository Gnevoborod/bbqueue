namespace bbqueue.Controllers.Dtos.Target
{
    internal sealed class TargetListDto
    {
        public List<TargetDto>? Targets { get; private set; }
        public TargetListDto(int count)
        {
            Targets = new List<TargetDto>(count);
        }
    }
}
