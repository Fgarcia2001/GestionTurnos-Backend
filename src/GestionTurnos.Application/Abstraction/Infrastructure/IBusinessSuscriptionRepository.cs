using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface IBusinessSubscriptionRepository : IBaseRepository<BusinessSubscription>
    {
        Task<List<BusinessSubscription>> GetActiveSubscriptionsAsync();

        Task UpdateAsync(BusinessSubscription entity);
    }
}
