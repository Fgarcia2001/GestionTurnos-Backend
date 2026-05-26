using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
