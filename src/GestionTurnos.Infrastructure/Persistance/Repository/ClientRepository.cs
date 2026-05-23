using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(FMCTurnosDbContext context) : base(context)
        {
        }
        public Client? GetClientByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name.Contains(name) && !x.IsDeleted);
        }

       
        public List<Client> GetAllGlobal()
        {
            return _context.Clients
                           .IgnoreQueryFilters() 
                           .Where(x => !x.IsDeleted)
                           .Include(x => x.Business) 
                           .ToList();
        }
    }
}
