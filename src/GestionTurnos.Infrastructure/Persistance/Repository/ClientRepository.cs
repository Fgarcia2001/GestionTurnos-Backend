using GestionTurnos.Aplication.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class ClientRepository : StaffRepository<Client>, IClientRepository
    {
        public ClientRepository(FMCTurnosDbContext context) : base(context)
        {
        }
    }
}
