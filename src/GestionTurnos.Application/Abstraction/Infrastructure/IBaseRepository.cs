using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T? GetById(Guid Id);
        T Add(T entity);
        void Update(T entity);
        void Delete(Guid Id);
    }
}
