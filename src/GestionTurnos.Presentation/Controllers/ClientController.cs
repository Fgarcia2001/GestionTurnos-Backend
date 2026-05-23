using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionTurnos.Presentation.Controllers
{
    
    [Authorize(Roles = Policies.Admin)]
    [Authorize(Roles = Policies.Recepcionista)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

      

        [HttpGet] 
        public ActionResult<List<ClientsResponse>> GetAll()
        {
          
            var clients = _clientService.GetClientsOfCurrentBusiness();
            return Ok(clients);
        }

        [HttpGet("{id}")] 
        public ActionResult<ClientsResponse> GetById([FromRoute] Guid id)
        {
            var client = _clientService.GetById(id);
            return Ok(client);
        }

        [HttpGet("search")] 
        public ActionResult<ClientsResponse> GetByName([FromQuery] string name)
        {
            var client = _clientService.GetByName(name);
            return Ok(client);
        }

        [HttpPost]
        public ActionResult<ClientsResponse> Create([FromBody] ClientRequest request)
        {
            
            var newClient = _clientService.CreateClient(request);
            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")] 
        public ActionResult Update([FromBody] ClientRequest request, [FromRoute] Guid id)
        {
            _clientService.UpdateClient(request, id);
            return NoContent();
        }

        [HttpDelete("{id}")] 
        public ActionResult Delete([FromRoute] Guid id)
        {
            _clientService.DeleteClient(id);
            return NoContent();
        }
    }
}