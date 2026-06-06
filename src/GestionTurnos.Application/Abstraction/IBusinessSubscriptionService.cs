using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Abstraction
{
    public interface IBusinessSubscriptionService
    {
        List<BusinessSubscriptionResponse> GetAll();
        BusinessSubscriptionResponse GetById(Guid id);
        List<BusinessSubscriptionResponse> GetByBusinessId(Guid businessId);
        BusinessSubscriptionResponse Create(BusinessSubscriptionRequest request);
        BusinessSubscriptionResponse UpdateStatus(Guid id, string status);
        void Delete(Guid id);
        void InitialBusinessSubscription(Plan plan, Business newBusiness);
    }
}
