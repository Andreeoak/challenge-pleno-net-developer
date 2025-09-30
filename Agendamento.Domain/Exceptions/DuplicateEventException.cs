using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agendamento.Domain.Exceptions;
    public class DuplicateEventException : Exception
    {
        public DuplicateEventException() : base("Já existe um agendamento para esta data e endereço.")
        {
        }
        public DuplicateEventException(string message) : base(message)
        {
        }
        public DuplicateEventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

