using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Infrastructure.Persistance;
using GestionTurnos.Infrastructure.Persistance.Repository;
using Microsoft.EntityFrameworkCore;

public class BusinessRepository : BaseRepository<Business>, IBusinessRepository
{
    private readonly ITenantProvider _tenantProvider;

    public BusinessRepository(FMCTurnosDbContext context, ITenantProvider tenantProvider) : base(context)
    {
        _tenantProvider = tenantProvider;
    }

    public Business? GetBusinessWithEcosystem()
    {
        var currentTenantId = _tenantProvider.GetBusinessId();

        return _context.Businesses
                       .Include(b => b.Branches)
                       .Include(b => b.Services)
                       .Include(b => b.Clients)
                       .FirstOrDefault(b => b.Id == currentTenantId && !b.IsDeleted);
    }

    public List<Business> GetAllGlobal()
    {
        return _context.Businesses
                       .IgnoreQueryFilters()
                       .Where(b => !b.IsDeleted)
                       .ToList();
    }
}