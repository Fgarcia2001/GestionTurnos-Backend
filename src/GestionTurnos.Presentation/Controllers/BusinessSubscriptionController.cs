using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Exceptions;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionTurnos.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessSubscriptionController : ControllerBase
    {
        private readonly IBusinessSubscriptionService _subscriptionService;
        private readonly ITenantProvider _tenantProvider;

        public BusinessSubscriptionController(
            IBusinessSubscriptionService subscriptionService,
            ITenantProvider tenantProvider)
        {
            _subscriptionService = subscriptionService;
            _tenantProvider = tenantProvider;
        }

        // Retorna todas las suscripciones para control del SysAdmin
        [Authorize(Policy = Policies.SysAdmin)]
        [HttpGet]
        public ActionResult<List<BusinessSubscriptionResponse>> GetAll()
        {
            try
            {
                var subscriptions = _subscriptionService.GetAll();
                return Ok(subscriptions);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        //Retorna una suscripcion a traves del Id del Business
        [Authorize(Policy = Policies.SysAdmin)]
        [HttpGet("{id}")]
        public ActionResult<BusinessSubscriptionResponse> GetById([FromRoute] Guid id)
        {
            try
            {
                var subscription = _subscriptionService.GetById(id);
                return Ok(subscription);
            }
            catch (ConflictException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        //Retorna la suscripcion actual del Business (para Admin)
        [Authorize(Policy = Policies.Admin)]
        [HttpGet("my")]
        public ActionResult<List<BusinessSubscriptionResponse>> GetMy()
        {
            try
            {
                var businessId = _tenantProvider.GetBusinessId()
                    ?? throw new ConflictException("No se encontró el negocio en el token.");

                var subscriptions = _subscriptionService.GetByBusinessId(businessId);
                return Ok(subscriptions);
            }
            catch (ConflictException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        //Crea una suscripcion, asignando un plan a un business
        [Authorize(Policy = Policies.SysAdmin)]
        [HttpPost]
        public ActionResult<BusinessSubscriptionResponse> Create([FromBody] BusinessSubscriptionRequest request)
        {
            try
            {
                var newSubscription = _subscriptionService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = newSubscription.Id }, newSubscription);
            }
            catch (ConflictException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        // Cambiar el estado de una suscripcion
        [Authorize(Policy = Policies.SysAdmin)]
        [HttpPut("{id}/status")]
        public ActionResult<BusinessSubscriptionResponse> UpdateStatus(
            [FromRoute] Guid id,
            [FromBody] UpdateSubscriptionStatusRequest request)
        {
            try
            {
                var updated = _subscriptionService.UpdateStatus(id, request.Status);
                return Ok(updated);
            }
            catch (ConflictException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        //Desactiva una suscripcion 
        [Authorize(Policy = Policies.SysAdmin)]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _subscriptionService.Delete(id);
                return NoContent();
            }
            catch (ConflictException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }
    }
}
