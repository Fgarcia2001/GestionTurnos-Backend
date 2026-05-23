using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Mapper;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        // Ya NO inyectamos _context ni _unitOfWork acá. El repositorio se encarga.
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientsResponse CreateClient(ClientRequest request)
        {
          
            var client = request.ToEntity();
         
            _clientRepository.Add(client);

            return client.ToResponse();
        }

        public List<ClientsResponse> GetClientsOfCurrentBusiness()
        {
            var clients = _clientRepository.GetAll();
            return clients.Select(c => c.ToResponse()).ToList();
        }

        public ClientsResponse GetById(Guid id)
        {
            var client = _clientRepository.GetById(id)
                ?? throw new KeyNotFoundException("Cliente no encontrado o no pertenece a su comercio.");
            return client.ToResponse();
        }

        public ClientsResponse GetByName(string name)
        {
            var client = _clientRepository.GetClientByName(name)
                ?? throw new KeyNotFoundException("Cliente no encontrado en su comercio.");
            return client.ToResponse();
        }

        public void UpdateClient(ClientRequest request, Guid id)
        {
            var existingClient = _clientRepository.GetById(id)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");

            
            existingClient.UpdateFromDto(request);

            
            _clientRepository.Update(existingClient);
        }

        public void DeleteClient(Guid id)
        {
            var existingClient = _clientRepository.GetById(id)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");

            _clientRepository.Delete(id);
        }


        public List<GlobalClientResponse> GetAllGlobal()
        {
            var allClients = _clientRepository.GetAllGlobal();
  
            return allClients.Select(c => c.ToGlobalResponse()).ToList();
        }
    }
}