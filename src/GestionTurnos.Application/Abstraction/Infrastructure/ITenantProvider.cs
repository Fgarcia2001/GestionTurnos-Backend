namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface ITenantProvider
    {
        Guid? GetBusinessId();
    }
}