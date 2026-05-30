using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Exceptions;
using GestionTurnos.Application.Mapper;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Services
{
    public class BusinessSubscriptionService : IBusinessSubscriptionService
    {
        private readonly IBusinessSubscriptionRepository _subscriptionRepository;

        public BusinessSubscriptionService(IBusinessSubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public List<BusinessSubscriptionResponse> GetAll()
        {
            return _subscriptionRepository.GetAllWithDetails()
                .Select(s => s.ToBusinessSubscriptionResponse())
                .ToList();
        }

        public BusinessSubscriptionResponse GetById(Guid id)
        {
            var subscription = _subscriptionRepository.GetByIdWithDetails(id)
                ?? throw new ConflictException("Suscripción no encontrada.");

            return subscription.ToBusinessSubscriptionResponse();
        }

        public List<BusinessSubscriptionResponse> GetByBusinessId(Guid businessId)
        {
            return _subscriptionRepository.GetByBusinessId(businessId)
                .Select(s => s.ToBusinessSubscriptionResponse())
                .ToList();
        }

        public BusinessSubscriptionResponse Create(BusinessSubscriptionRequest request)
        {
            var newSubscription = request.ToBusinessSubscription();
            _subscriptionRepository.Add(newSubscription);

            // Reload with details so the response has Business/Plan names
            var created = _subscriptionRepository.GetByIdWithDetails(newSubscription.Id)
                ?? throw new ConflictException("Error al obtener la suscripción creada.");

            return created.ToBusinessSubscriptionResponse();
        }

        public BusinessSubscriptionResponse UpdateStatus(Guid id, string status)
        {
            if (!Enum.TryParse<Status>(status, ignoreCase: true, out var parsedStatus))
                throw new ConflictException($"Estado inválido: '{status}'. Los valores permitidos son: {string.Join(", ", Enum.GetNames<Status>())}.");

            var subscription = _subscriptionRepository.GetByIdWithDetails(id)
                ?? throw new ConflictException("Suscripción no encontrada.");

            subscription.Status = parsedStatus;
            subscription.UpdateDateTime = DateTime.Now;
            _subscriptionRepository.Update(subscription);

            return subscription.ToBusinessSubscriptionResponse();
        }

        public void Delete(Guid id)
        {
            var subscription = _subscriptionRepository.GetById(id)
                ?? throw new ConflictException("Suscripción no encontrada.");

            _subscriptionRepository.Delete(id);
        }
    }
}
