using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Abstraction.Infrastructure;
using GestionTurnos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Application.Services
{
    public class BusinessSubscriptionService : IBusinessSubscriptionService
    {
        private readonly IBusinessSubscriptionRepository _Businessrepository;

        public BusinessSubscriptionService(IBusinessSubscriptionRepository Businessrepository)
        {
            _Businessrepository = Businessrepository;
        }

        public BusinessSubscription CreateBusinessSubscription(BusinessSubscription BusinessSubscription)
        {
            return _Businessrepository.Add(BusinessSubscription);
        }


    }
}
