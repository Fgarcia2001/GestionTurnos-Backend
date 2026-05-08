using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Aplication.Abstraction
{
    public interface IBusinessService
    {

        List<Business> GetAll();
        Business GetById(Guid id);

        Business Create(Business business);

        Business Update(Guid id, string value);

        bool Delete (Guid id);
    }
}
