﻿using System.ComponentModel.DataAnnotations;

namespace bbqueue.Domain.Models
{
    public enum WindowWorkState { Opened, Suspended, Closed}
    internal sealed class Window
    {
        public long Id { get; set; }

        [MaxLength(6)]
        public string Number { get; set; } = null!;

        [MaxLength(256)]
        public string? Description { get; set; }

        public long? EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public WindowWorkState WindowWorkState { get; set;}
    }
}