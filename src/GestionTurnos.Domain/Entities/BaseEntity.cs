using System.ComponentModel.DataAnnotations;

namespace GestionTurnos.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public bool IsDeleted { get; set; } = false;
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
        public DateTime? DeleteDateTime { get; set; }
    }
}
