using System.ComponentModel.DataAnnotations;

namespace GestionTurnos.Application.Request
{
    public class PlanRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationDays { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
