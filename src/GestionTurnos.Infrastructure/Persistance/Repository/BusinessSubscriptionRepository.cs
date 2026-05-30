using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class BusinessSubscriptionRepository : BaseRepository<BusinessSubscription>, IBusinessSubscriptionRepository
    {
        public BusinessSubscriptionRepository(FMCTurnosDbContext context) : base(context)
        {
        }

        public async Task<List<BusinessSubscription>> GetActiveSubscriptionsAsync()
        {
            return await _dbSet
                .Include(bs => bs.Business)
                .Where(bs => bs.Status == Status.Active)
                .ToListAsync();
        }

        public async Task UpdateAsync(BusinessSubscription entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public List<BusinessSubscription> GetAllWithDetails()
        {
            return _dbSet
                .Include(bs => bs.Business)
                .Include(bs => bs.Plan)
                .Where(bs => !bs.IsDeleted)
                .ToList();
        }

        public BusinessSubscription? GetByIdWithDetails(Guid id)
        {
            return _dbSet
                .Include(bs => bs.Business)
                .Include(bs => bs.Plan)
                .FirstOrDefault(bs => bs.Id == id);
        }

        public List<BusinessSubscription> GetByBusinessId(Guid businessId)
        {
            return _dbSet
                .Include(bs => bs.Business)
                .Include(bs => bs.Plan)
                .Where(bs => bs.BusinessId == businessId && !bs.IsDeleted)
                .ToList();
        }
    }
}
