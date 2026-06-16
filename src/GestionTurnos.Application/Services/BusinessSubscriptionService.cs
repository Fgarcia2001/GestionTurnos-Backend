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
        private readonly IPlanService _planService;

        public BusinessSubscriptionService(IBusinessSubscriptionRepository subscriptionRepository, IPlanService planService)
        {
            _subscriptionRepository = subscriptionRepository;
            _planService = planService;
        }

        private BusinessSubscription GetCurrentSubscriptionEntity(Guid businessId)
        {
            return _subscriptionRepository
                .GetCurrentSubscription(businessId)
                ?? throw new ConflictException("El negocio no posee una suscripcion activa");
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

            if (parsedStatus == Status.Expired)
            {
                throw new ConflictException("El estado Expired es administrado automaticamente por el sistema");
            }

            subscription.Status = parsedStatus;

            _subscriptionRepository.Update(subscription);

            return subscription.ToBusinessSubscriptionResponse();
        }

        public void Delete(Guid id)
        {
            var subscription = _subscriptionRepository.GetById(id)
                ?? throw new ConflictException("Suscripción no encontrada.");

            _subscriptionRepository.Delete(id);
        }


        public void InitialBusinessSubscription(Plan plan, Business newBusiness)
        {
            var BusinessSubscription = new BusinessSubscription
            {
                Id = Guid.NewGuid(),
                BusinessId = newBusiness.Id,
                Business = newBusiness,
                PlanId = plan.Id,
                Plan = plan,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow + TimeSpan.FromDays(plan.DurationDays),
                Status = Status.Active
            };
            _subscriptionRepository.Add(BusinessSubscription);
        }


        public BusinessSubscriptionResponse GetCurrentSubscription(Guid businessId)
        {
            return GetCurrentSubscriptionEntity(businessId).ToBusinessSubscriptionResponse();
        }

        public void RenewSubscription(Guid businessId)
        {
            var subscription = _subscriptionRepository
                .GetLatestByBusinessId(businessId)
                ?? throw new ConflictException("El negocio no posee suscripciones");

            if(subscription.Status == Status.Cancelled)
            {
                throw new ConflictException("No se puede renovar una suscripcion cancelada");
            }

            if (subscription.Plan == null)
            {
                throw new ConflictException("La suscripción no tiene un plan asociado.");
            }

            //logica para que si ya pago el mes y quiere renovar antes que termine, no pierda los dias, sino que extienda
            if(subscription.EndDate > DateTime.UtcNow)
            {
                subscription.EndDate = subscription.EndDate.AddDays(subscription.Plan.DurationDays);
            }
            else
            {
                subscription.StartDate = DateTime.UtcNow;
                subscription.EndDate = DateTime.UtcNow.AddDays(subscription.Plan.DurationDays);
            }

            subscription.Status = Status.Active;

            _subscriptionRepository.Update(subscription);
        }

        public void ChangePlan(Guid businessId, Guid newPlanId)
        {
            var currentSubscription = _subscriptionRepository
                .GetLatestByBusinessId(businessId)
                ?? throw new NotFoundException("El negocio no posee suscripciones");

            var newPlan = _planService.GetActivePlan(newPlanId);

            if(currentSubscription.PlanId == newPlan.Id)
            {
                throw new ConflictException("El negocio ya posee este plan");
            }

            currentSubscription.Status = Status.Inactive;

            _subscriptionRepository.Update(currentSubscription);

            var newSubscription = new BusinessSubscription
            {
                BusinessId = businessId,
                PlanId = newPlan.Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(newPlan.DurationDays),
                Status = Status.Active
            };

            _subscriptionRepository.Add(newSubscription);
            
        }
    }
}
