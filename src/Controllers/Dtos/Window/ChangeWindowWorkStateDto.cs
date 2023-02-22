using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Window
{
    public sealed class ChangeWindowWorkStateDto
    {
        [Required, MaxLength(6)]
        public string Number { get; set; } = default!;
        [Required, MinValue((int)WindowWorkState.Opened), MaxValue((int)WindowWorkState.Closed)]
        public WindowWorkState WindowWorkState { get; set; }
    }
}
