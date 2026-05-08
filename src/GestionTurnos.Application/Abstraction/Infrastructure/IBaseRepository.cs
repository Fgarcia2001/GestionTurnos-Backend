using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Aplication.Abstraction.Infrastructure
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T? GetById(Guid id);
        T Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
