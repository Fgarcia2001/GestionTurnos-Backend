using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface IBusinessSubscriptionRepository : IBaseRepository<BusinessSubscription>
    {
        Task<List<BusinessSubscription>> GetActiveSubscriptionsAsync();
        Task UpdateAsync(BusinessSubscription entity);
        List<BusinessSubscription> GetAllWithDetails();
        BusinessSubscription? GetByIdWithDetails(Guid id);
        List<BusinessSubscription> GetByBusinessId(Guid businessId);
    }
}
