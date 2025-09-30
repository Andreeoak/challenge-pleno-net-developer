
namespace Agendamento.Domain.Interfaces;
    public interface IAgendamentoValidator
    {
        public bool ValidaData(DateTime? data);
        public bool ValidaCliente(string? cliente);
        public bool ValidaEvento(DateTime? data, string? endereco);
    }

