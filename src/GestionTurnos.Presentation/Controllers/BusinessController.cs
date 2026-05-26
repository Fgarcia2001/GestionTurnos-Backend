using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Exceptions;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionTurnos.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        
        [HttpGet("global")]
        public ActionResult<List<Business>> GetAllGlobal()
        {
            try
            {
            return Ok(_businessService.GetAllGlobal());
            }
            catch(Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }




        [HttpGet("MyBusiness")] 
        public ActionResult<BusinessDashboardResponse> GetMyBusinessWithEcosystem()
        {
            try
            {
                var businessEcosystem = _businessService.GetBusinessEcosystem();
                return Ok(businessEcosystem);
            }catch(ConflictException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
            return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [Authorize(Roles = Policies.Admin)]
        [HttpPut("/MyBusiness/Update")]
        public ActionResult UpdateMyBusiness([FromBody] BusinessUpdateRequest request)
        {
            _businessService.Update(request);
            return NoContent();
        }

        [HttpDelete("/MyBusiness/Delete")]
        public ActionResult<bool> Delete()
        {
            try
            {
                _businessService.Delete();
                return Ok(true);
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