using System.ComponentModel.DataAnnotations;

namespace GestionTurnos.Application.Request
{
    public class BusinessSubscriptionRequest
    {
        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid PlanId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
