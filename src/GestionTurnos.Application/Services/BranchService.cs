using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Application.Mapper;
using GestionTurnos.Application.Response;
using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public List<BranchResponse> GetAll()
        {
            return _branchRepository
                .GetAll()
                .OrderBy(x => x.Name)
                .Select(x => x.ToBranchResponse())
                .ToList();
        }
        
    }
}
