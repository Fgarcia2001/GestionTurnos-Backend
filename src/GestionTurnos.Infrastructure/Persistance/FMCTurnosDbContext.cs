using GestionTurnos.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GestionTurnos.Infrastructure.Persistance
{
    public class FMCTurnosDbContext : DbContext
    {
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Staff> Staffs { get; set; } // O simplemente Staff
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<BusinessSubscription> BusinessSubscriptions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public FMCTurnosDbContext(DbContextOptions<FMCTurnosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuramos la relación de Appointments para evitar ciclos de cascada
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.NoAction); // <--- Esto rompe el ciclo

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.NoAction); // <--- Esto también ayuda

            // Si tenés otras relaciones que den problemas, podés sumarlas acá
        }
    }
}
