using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GestionTurnos.Domain.Entities
{

    public enum Rol { Sysadmin,Admin,Profesional}
    public class Staff : BaseEntity
    {
        [Required]
        public Guid BusinessId { get; set; }
        public Business Business { get; set; } = null!;
        public Guid? BranchId { get; set; }
        public  Branch? Branch { get; set; } = null;

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [Phone]
        public string Phone {  get; set; } = string.Empty;

        public string LinkPhoto { get; set; } = string.Empty;

        public Rol Rol { get; set; }

        // Propiedad de navegación inversa: Un profesional tiene muchos turnos
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
