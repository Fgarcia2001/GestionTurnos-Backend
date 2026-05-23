using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Application.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception inner) : base(message, inner) { }
    }
}
