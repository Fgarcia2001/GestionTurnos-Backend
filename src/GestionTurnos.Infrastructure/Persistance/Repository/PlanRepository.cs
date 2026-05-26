using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Infrastructure.Persistance.Repository
{
    public class PlanRepository :BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(FMCTurnosDbContext context) : base(context)
        {
        }
    }
}
