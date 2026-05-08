using System.ComponentModel.DataAnnotations;

namespace GestionTurnos.Domain.Entities
{
    public enum Status { Habilitado,Deshabilitado }
    public enum TypeBusiness { Barberia, Spa}
    public class Business : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Url { get; set; } = string.Empty;
        public string? UrlLogo { get; set; }
        public bool IsActive { get; set; } = true;
        public TypeBusiness TypeBusiness { get; set; }

        // Propiedad de navegación inversa: Un negocio tiene muchas sucursales
        public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

        // Relación con Clientes
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    }
}
