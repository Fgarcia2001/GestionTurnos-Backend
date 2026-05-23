using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Client? GetClientByName(string name);

        List<Client> GetAllGlobal();

     
    }
}
