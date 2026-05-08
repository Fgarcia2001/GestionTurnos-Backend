using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTurnos.Domain.Entities
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public enum PaymentMethod
    {
        Cash,
        MercadoPago,
        Transfer
    }
    public class Appointment : BaseEntity
    {
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null!;


        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;

    
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;


        public DateTime Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Observation { get; set; }
        public PaymentMethod Payment { get; set; } 
        public AppointmentStatus Status { get; set; }
        public double TotalCost { get; set; }
    }
}
