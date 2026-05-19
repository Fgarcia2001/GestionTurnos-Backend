using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Request;
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
        [Authorize(Policy = Policies.Sysadmin)]
        [HttpGet]
        public ActionResult<List<Staff>> GetAll()
        {

            return Ok(_staffService.GetAll());
        }
        [Authorize(Policy = Policies.Sysadmin)]
        [HttpGet("{id}")]
        public ActionResult<Staff> GetById(Guid id)
        {

            return Ok(_staffService.GetById(id));
        }
        [Authorize(Policy = Policies.Sysadmin)]
        [HttpGet("Business/{businessId}")]
        public ActionResult<List<Staff>> GetStaffOfBusiness(Guid businessId)
        {

            return Ok(_staffService.GetStaffOfBusiness(businessId));
        }
        [Authorize(Policy = Policies.Sysadmin)]
        [HttpPost]
        public ActionResult<Staff> CreateStaff([FromBody] StaffRequest Staff)
        {
           return Ok(_staffService.CreateStaff(Staff, Staff.BusinessId));

        }

        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        public ActionResult DeleteStaff(Guid id)
        {
            _staffService.DeleteStaff(id);
            return NoContent();
        }
        [Authorize(Policy = Policies.Sysadmin)] // ejemplo 1
        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        public ActionResult<Staff> UpdateStaff([FromBody] StaffRequest Staff, Guid id)
        {
            var updatedUser = _staffService.UpdateStaff(Staff, id);
            return Ok(updatedUser);
        }
    }
}
