using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Window
{
    public class WindowCreateDto
    {
        [MaxLength(6)]
        public string Number { get; set; } = default!;
        [MaxLength(256)]
        public string? Description { get; set; }

    }
}
