using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface ISysAdminRepository : IBaseRepository<SysAdminUser>
    {
        SysAdminUser? GetByEmail(string email);
    }
}
