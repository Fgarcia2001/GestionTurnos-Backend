using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestionTurnos.Domain.Entities
{
    public class Client :BaseEntity
    {
        public required string Name { get; set; } = string.Empty;

        [EmailAddress]
        public required string Email { get; set; } = string.Empty;
        [Phone]
        public required string Phone { get; set; } = string.Empty;
        [Required]
        public Guid BusinessId { get; set; }
        public  Business Business { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        // Propiedades de navegación inversa
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
       


    }
}
