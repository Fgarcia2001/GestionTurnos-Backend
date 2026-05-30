using System.ComponentModel.DataAnnotations;

namespace GestionTurnos.Application.Request
{
    public class UpdateSubscriptionStatusRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
