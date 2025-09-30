namespace Agendamento.Domain.Exceptions
{
    public class InvalidClientException : Exception
    {
        public InvalidClientException() : base("O nome do cliente não pode ser vazio.")
        {
        }
        public InvalidClientException(string message) : base(message)
        {
        }
        public InvalidClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
