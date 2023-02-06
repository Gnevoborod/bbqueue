using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("window")]
	public class Window
	{
        [Column("window_id")]
        public int Id { get; set; }

        [Column("number")]
        public string Number { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("employee_id")]
        public Employee? Employee { get; set; }
	}
}