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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        
        [Authorize(Policy = Policies.Admin)]
        [HttpGet("Business/Staffs")]
        public ActionResult<List<StaffsResponse>> GetStaffOfBusiness() 
        {

            try
            {
                var staffs = _staffService.GetStaffOfCurrentBusiness();
                return Ok(staffs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado y no se pudo obtener la lista de personal.");
            }
        }
        [Authorize(Policy = Policies.Admin)]
        [HttpPost]
        public ActionResult<StaffsResponse> CreateStaff([FromBody] StaffRequest Staff)
        {
            try { 
            var createdStaff = _staffService.CreateStaff(Staff);
            return Ok(createdStaff);
            }
            catch(ConflictException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        public ActionResult DeleteStaff([FromRoute] Guid id)
        {
            try
            {
                _staffService.DeleteStaff(id);
                return NoContent();
            }
            catch (ConflictException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        public ActionResult<Staff> UpdateStaff([FromBody] StaffRequest Staff, [FromRoute] Guid id)
        {
            var updatedUser = _staffService.UpdateStaff(Staff, id);
            return Ok(updatedUser);
        }
    }
}
