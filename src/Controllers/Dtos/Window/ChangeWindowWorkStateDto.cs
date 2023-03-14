using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Validators;
using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Window
{
    public sealed class ChangeWindowWorkStateDto
    {
        [Required, MaxLength(6)]
        public string Number { get; set; } = default!;
        [Required]
        [EnumValueScope(typeof(WindowWorkState))]
        public string WindowWorkState { get; set; } = default!;
    }
}
