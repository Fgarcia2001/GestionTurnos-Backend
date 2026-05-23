using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
        {
            public StaffRepository(FMCTurnosDbContext context) : base(context)
            {
            }

        public List<Staff> GetAllGlobal()
        {
            return _context.Staffs
                           .IgnoreQueryFilters() 
                           .Include(s => s.Business) 
                           .ToList();
        }
    }
       
    
}
