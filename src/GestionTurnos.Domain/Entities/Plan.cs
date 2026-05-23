using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionTurnos.Domain.Entities
{
    public class Plan : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int DurationDays { get; set; }

        public bool IsActive { get; set; } = true;

        // Propiedad de navegación inversa
        public virtual ICollection<BusinessSubscription> Subscriptions { get; set; } = new List<BusinessSubscription>();
    }
}
