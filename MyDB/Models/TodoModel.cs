using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        public DateTime? Done { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Too short todo message")]
        [MaxLength(30, ErrorMessage = "Too long todo message")]
        public string? Message { get; set; }
        public DateTime? Created { get; set; }
    }
}
    