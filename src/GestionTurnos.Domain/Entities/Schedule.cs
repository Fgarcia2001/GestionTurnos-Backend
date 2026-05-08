using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestionTurnos.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        [Required]
        public Branch Branch { get; set; } = null!;

        public string? DaysClosedOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double IntervalHour { get; set; }
    }
}
