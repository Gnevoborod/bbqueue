namespace bbqueue.Controllers.Dtos.Window
{
    internal sealed class WindowListDto
    {
        public List<WindowDto>? Windows { get; private set; }
        public WindowListDto(int count)
        {
            Windows = new List<WindowDto>(count);
        }
    }
}
