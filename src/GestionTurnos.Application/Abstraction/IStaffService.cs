using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;

namespace GestionTurnos.Application.Abstraction
{
    public interface IStaffService
    {
        List<StaffsResponse> GetStaffOfCurrentBusiness();
        StaffsResponse GetById(Guid id);
        StaffsResponse CreateStaff(StaffRequest request);
        StaffsResponse UpdateStaff(StaffRequest staff, Guid idStaff);
        void DeleteStaff(Guid id);
        List<GlobalStaffResponse> GetAllGlobal();
    }
}