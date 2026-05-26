using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
        {
        private readonly ITenantProvider _tenantProvider;
        public StaffRepository(FMCTurnosDbContext context, ITenantProvider tenantProvider) : base(context)
            {
                _tenantProvider = tenantProvider;
            }

        public List<Staff> GetAll()
        {
            return _dbSet.Where(s =>s.BusinessId == _tenantProvider.GetBusinessId() && !s.IsDeleted)
                .Include(s => s.Branch)
                .ToList();
        }
    }
       
    
}
