using GestionTurnos.Aplication.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class StaffRepository : StaffRepository<Staff>, IStaffRepository
        {
            public StaffRepository(FMCTurnosDbContext context) : base(context)
            {
            }
        }
       
    
}
