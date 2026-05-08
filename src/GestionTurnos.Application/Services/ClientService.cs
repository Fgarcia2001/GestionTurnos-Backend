using GestionTurnos.Aplication.Abstraction;
using GestionTurnos.Aplication.Abstraction.Infrastructure;
using GestionTurnos.Aplication.Request;
using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Aplication.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public Client CreateClient(BusinessRequest request, Guid id_Business)
        {
            throw new NotImplementedException();
        }

        public Client CreateUser(BusinessRequest request)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Client UpdateUser(Client user)
        {
            throw new NotImplementedException();
        }
    }
}
