using GestionTurnos.Aplication.Abstraction;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Aplication.Services
{
    public class BusinessService : IBusinessService
    {
        /*
         public List<Business> GetAll()
         {
             return _businesses;
         }

         public Business Create(Business business)
         {
             _businesses.Add(business);
             return business;
         }

         public bool Delete(Guid id)
         {
             var businessDeleted = _businesses.FirstOrDefault(x => x.Id == id);

             if (businessDeleted is null){
                 return false;
             } 

             _businesses.Remove(businessDeleted);
             return true;
         }

         public Business GetById(Guid id)
         {
             var businessById = _businesses.FirstOrDefault(x => x.Id == id);
             if(businessById is null)
             {
                 throw new NotImplementedException();
             }
             return businessById;
         }

         public Business Update(Guid id, string value)
         {
             return new Business();
         }*/
        public Business Create(Business business)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Business> GetAll()
        {
            throw new NotImplementedException();
        }

        public Business GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Business Update(Guid id, string value)
        {
            throw new NotImplementedException();
        }
    }
}