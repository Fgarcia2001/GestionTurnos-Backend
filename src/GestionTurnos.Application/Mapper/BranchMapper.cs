using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;

namespace GestionTurnos.Application.Mapper
{
    public static class BranchMapper
    {
        public static BranchResponse ToBranchResponse(this Branch branch)
        {
            return new BranchResponse
            {
                Name = branch.Name,
                Address = branch.Address,
                Phone = branch.Phone,
                City = branch.City

            };
        }
    }
}
