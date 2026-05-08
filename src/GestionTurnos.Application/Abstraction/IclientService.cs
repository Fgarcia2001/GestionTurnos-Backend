using GestionTurnos.Aplication.Request;
using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Aplication.Abstraction
{
    public interface IClientService 
    {
        public List<Client> GetAll();
        public Client GetById(Guid id);

        public Client CreateUser(BusinessRequest request);

        public Client CreateClient(BusinessRequest request, Guid id_Business);

        public Client UpdateUser(Client user);
        public bool DeleteUser(Guid id);
    }
}
