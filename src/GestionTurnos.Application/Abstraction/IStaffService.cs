

using GestionTurnos.Aplication.Request;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Aplication.Abstraction
{
    public interface IStaffService
    {
        public List<Staff> GetAll();
        public Staff GetById(Guid id);

        public Staff CreateUser(BusinessRequest request);

        public Staff CreateStaff(BusinessRequest request, Guid id_Business); 

        public Staff UpdateUser(Staff user, Rol? rol);
        public void DeleteUser(Guid id);

    }
}
