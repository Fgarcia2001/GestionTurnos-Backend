using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Application.Abstraction.Infrastructure.External_Interface
{
    public interface IDolarPriceService
    {
        Task<decimal> CurrentDolarPrice();
    }
}
