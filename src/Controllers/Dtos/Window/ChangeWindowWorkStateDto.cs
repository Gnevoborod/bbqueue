using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Window
{
    public sealed class ChangeWindowWorkStateDto
    {
        [Required, MaxLength(6)]
        public string Number { get; set; } = default!;
        [Required]
        [StringValueScope("Opened","Closed","Suspended")]
        public string WindowWorkState { get; set; } = default!;
    }
}
