using GestionTurnos.Aplication.Abstraction;
using GestionTurnos.Aplication.Abstraction.Infrastructure;
using GestionTurnos.Aplication.Request;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Aplication.Services
{
    public class StaffService : IStaffService
    {

        private readonly IStaffRepository _staffRepository;
        private readonly I
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }



        // Lista estática para persistir datos en memoria durante la ejecución
        
         public Staff CreateStaff(BusinessRequest request, Guid id_Business)
         {

             var newStaff = new Staff
             {
                 Id = Guid.NewGuid(),
                 Name = request.Name,
                 Email = request.Email,
                 Password = request.Password,
                 Phone = request.Phone,
                 Rol = request.Rol,
                 LinkPhoto = request.LinkPhoto,
                 BusinessId = id_Business,

             };

             _staffRepository.Add(newStaff);
             return newStaff;
         }

         public Staff CreateUser(BusinessRequest request)
         {

             var newBusiness = new Business
             {
                 Id = Guid.NewGuid(),
                 Name = $"{request.Name} - {request.BusinessCategory}",
                 Url = $"http://www.{request.Name.Replace(" ", "")}.FCMTurniFy.com"
             };

             var newUser = new Staff
             {
                 Id = Guid.NewGuid(),
                 Name = request.Name,
                 Email = request.Email,
                 Password = request.Password,
                 Phone = request.Phone,
                 Rol = request.Rol,
                 LinkPhoto = request.LinkPhoto,
                 BusinessId = newBusiness.Id,
                 Business = newBusiness
             };

             _staffRepository.Add(newUser);
            // Add the Repository of Business (Micael)

             return newUser;
         }


         public void DeleteUser(Guid id)
         {
              _staffRepository.Delete(id);
         }

         // Si tu interfaz devuelve 'User' (singular), devolverá el primero de la lista.
         // Si debería ser una lista, cambia el retorno a List<User> en la Interfaz.
         public List<Staff> GetAll()
         {
             return _staffRepository.GetAll();
         }

         public Staff? GetById(Guid id)
         {
             return _staffRepository.GetById(id);
         }

         public Staff UpdateUser(Staff user, Rol? rol)
         {
             var existingUser = _staffRepository.GetById(user.Id);
             if (existingUser != null)
             {
                 existingUser.Name = user.Name;
                 existingUser.Email = user.Email;
                 existingUser.Phone = user.Phone;
                 existingUser.LinkPhoto = user.LinkPhoto;

                 if (rol.HasValue)
                 {
                     existingUser.Rol = rol.Value;
                 }

                 return existingUser;
             }
             throw new Exception("Usuario no encontrado");
         }
       
    }
}