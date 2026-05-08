using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestionTurnos.Domain.Entities
{
    public class BusinessSubscription : BaseEntity
    {   
        public Guid BusinessId { get; set; }
        public Business Business { get; set; } = null!;
        public Guid PlanID { get; set; }
        public Plan Plan { get; set; } = null!;



        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
    }
}
