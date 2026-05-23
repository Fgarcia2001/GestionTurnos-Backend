using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Response
{
    public class StaffsResponse
    {
        public Guid IdStaff { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public string StaffEmail { get; set; } = string.Empty;
        public string StaffLinkPhoto { get; set; } = string.Empty;
        public string StaffPhone { get; set; }
        public Rol Rol { get; set; }
        public string BranchId { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
    }
}