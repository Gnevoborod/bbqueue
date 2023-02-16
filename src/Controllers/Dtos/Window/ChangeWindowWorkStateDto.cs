using bbqueue.Domain.Models;

namespace bbqueue.Controllers.Dtos.Window
{
    public sealed class ChangeWindowWorkStateDto
    {
        public string Number { get; set; }
        public WindowWorkState WindowWorkState { get; set; }

        public ChangeWindowWorkStateDto(string number, WindowWorkState windowWorkState)
        {
            Number= number;
            WindowWorkState= windowWorkState;
        }
    }
}
