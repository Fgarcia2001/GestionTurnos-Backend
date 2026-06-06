using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Exceptions;
using GestionTurnos.Application.Mapper;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public List<PlanResponse> GetAll()
        {
            return _planRepository.GetAllGlobal()
                .Select(p => p.ToPlanResponse())
                .ToList();
        }

        public PlanResponse GetById(Guid id)
        {
            var plan = _planRepository.GetById(id)
                ?? throw new ConflictException("Plan no encontrado.");

            return plan.ToPlanResponse();
        }

        public PlanResponse Create(PlanRequest request)
        {
            var newPlan = request.ToPlan();
            _planRepository.Add(newPlan);
            return newPlan.ToPlanResponse();
        }

        public PlanResponse Update(PlanRequest request, Guid id)
        {
            var existingPlan = _planRepository.GetById(id)
                ?? throw new ConflictException("Plan no encontrado.");

            existingPlan.UpdateFromRequest(request);
            _planRepository.Update(existingPlan);

            return existingPlan.ToPlanResponse();
        }

        public void Delete(Guid id)
        {
            var plan = _planRepository.GetById(id)
                ?? throw new ConflictException("Plan no encontrado.");

            _planRepository.Delete(id);
        }

        public Plan GetPlanOrDefault(Guid? planId)
        {
            if (!planId.HasValue || planId == Guid.Empty)
            {
                return _planRepository.GetAllGlobal()
                    .FirstOrDefault(p => p.Name == "Free Plan")
                    ?? _planRepository.GetAllGlobal().FirstOrDefault()
                    ?? throw new ConflictException("No se encontro plan.");
            }

            return _planRepository.GetById(planId.Value)
                ?? throw new ConflictException("El plan especificado no existe");
        }
    }
}
