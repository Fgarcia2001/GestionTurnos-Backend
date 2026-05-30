using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;

namespace GestionTurnos.Application.Abstraction
{
    public interface IPlanService
    {
        List<PlanResponse> GetAll();
        PlanResponse GetById(Guid id);
        PlanResponse Create(PlanRequest request);
        PlanResponse Update(PlanRequest request, Guid id);
        void Delete(Guid id);
    }
}
