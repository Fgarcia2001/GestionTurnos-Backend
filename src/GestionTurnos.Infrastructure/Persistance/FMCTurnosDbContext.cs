using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionTurnos.Infrastructure.Persistance
{
    public class FMCTurnosDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public FMCTurnosDbContext(DbContextOptions<FMCTurnosDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<BusinessSubscription> BusinessSubscriptions { get; set; }
        public DbSet<SysAdminUser> SysAdminUsers { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Herencia TPH - raíz User
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Staff>("Staff")
                .HasValue<Client>("Client")
                .HasValue<SysAdminUser>("SysAdmin");

            // 2. Query filter en la raíz (TPH solo permite uno por jerarquía)
            modelBuilder.Entity<User>().HasQueryFilter(u =>
                !u.IsDeleted && (
                    EF.Property<string>(u, "UserType") == "SysAdmin" ||
                    (u is Staff && ((Staff)(object)u).BusinessId == _tenantProvider.GetBusinessId()) ||
                    (u is Client && ((Client)(object)u).BusinessId == _tenantProvider.GetBusinessId())
                )
            );

            // 3. Forzar misma columna "BusinessId" para Staff y Client en la tabla TPH
            modelBuilder.Entity<Staff>()
                .Property(s => s.BusinessId)
                .HasColumnName("BusinessId");

            modelBuilder.Entity<Client>()
                .Property(c => c.BusinessId)
                .HasColumnName("BusinessId");

            // 4. Relación Staff -> Business
            // Business NO tiene ICollection<Staff>, WithMany() vacío
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Business)
                .WithMany()
                .HasForeignKey(s => s.BusinessId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Relación Client -> Business
            // Business SÍ tiene ICollection<Client>, conectamos ambos lados
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Business)
                .WithMany(b => b.Clients)
                .HasForeignKey(c => c.BusinessId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // 6. Precision para decimales
            modelBuilder.Entity<Plan>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Service>().Property(s => s.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Appointment>().Property(a => a.TotalCost).HasPrecision(18, 2);

            // 7. Relaciones de Appointment con Restrict
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}