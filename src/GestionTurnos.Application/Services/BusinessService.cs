using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Request;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly ITenantProvider _tenantProvider;

        public BusinessService(IBusinessRepository businessRepository, ITenantProvider tenantProvider)
        {
            _businessRepository = businessRepository;
            _tenantProvider = tenantProvider;
        }

        public Business Create(Business business)
        {
            return _businessRepository.Add(business);
        }

        public void Delete()
        {
            var BusinesId = _tenantProvider.GetBusinessId(); 
           
            _businessRepository.Delete(BusinesId ?? Guid.Empty);
        }

        public List<Business> GetAllGlobal()
        {
            return _businessRepository.GetAllGlobal();
        }

        public BusinessDashboardResponse GetBusinessEcosystem()
        {
            throw new NotImplementedException();
        }
        /* public BusinessDashboardResponse GetBusinessEcosystem()
         {

             var business = _businessRepository.GetBusinessWithEcosystem()
                 ?? throw new KeyNotFoundException("No se encontró la configuración de su empresa.");



             return new BusinessDashboardResponse
             {
                 Id = business.Id,
                 Name = business.Name,
                 Branches = business.Branches.Select(b => new BranchResponse
                 {
                     Id = b.Id,
                     Name = b.Name,
                     Address = b.Address
                 }).ToList(),
                 Services = business.Services.Select(s => new ServiceResponse
                 {
                     Id = s.Id,
                     Name = s.Name,
                     Price = s.Price,
                     DurationMinutes = s.Duration
                 }).ToList(),
                 Staff = business.Clients.Select(s => new StaffsResponse
                 {
                     IdStaff = s.Id,
                     StaffName = s.Name,
                     StaffEmail = s.Email,
                     StaffLinkPhoto = s.LinkPhoto,
                     StaffPhone = s.Phone,
                     Rol = s.Rol
                 }).ToList()
             };
         }*/

        public void Update(BusinessUpdateRequest value)
        {
            var BusinesId = _tenantProvider.GetBusinessId();

            var existingBusiness = _businessRepository.GetById(BusinesId ?? Guid.Empty)
                ?? throw new KeyNotFoundException("Empresa no encontrada");

            existingBusiness.Name = value.Name;
            

            _businessRepository.Update(existingBusiness);
        }
    }
}