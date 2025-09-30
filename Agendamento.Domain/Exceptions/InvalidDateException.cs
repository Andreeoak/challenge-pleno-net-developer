using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agendamento.Domain.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException() : base("A data do agendamento é inválida.")
        {
        }
        public InvalidDateException(string message) : base(message)
        {
        }
        public InvalidDateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
