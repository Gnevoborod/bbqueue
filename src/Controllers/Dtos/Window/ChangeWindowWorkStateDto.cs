using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Validators;
using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Window
{
    public sealed class ChangeWindowWorkStateDto
    {
        [Required, MaxLength(6)]
        public string number { get; set; } = default!;
        [Required]
        [StringValueScope("Opened","Closed","Suspended")]
        public string windowWorkState { get; set; } = default!;

    }
}
