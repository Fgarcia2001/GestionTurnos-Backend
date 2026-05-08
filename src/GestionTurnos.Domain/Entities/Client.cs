using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestionTurnos.Domain.Entities
{
    public class Client : BaseEntity
    {
        [Required]
        public Guid BusinessId { get; set; }
        public  Business Business { get; set; } = null!;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime BirthDay { get; set; }

        public int? Phone { get; set; }

        // Propiedades de navegación inversa
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
       


    }
}
