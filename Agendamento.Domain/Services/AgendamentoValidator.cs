using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Services;

    public class AgendamentoValidator: IAgendamentoValidator
    {
        private IAgendamentoRepository _repository { get; init; }
        
        public AgendamentoValidator(IAgendamentoRepository repository)
        {
            _repository = repository;
        }
        
        public bool ValidaData(DateTime? data)
        {
            ChecaDataVazia(data);
            ChecaDataNoPassado(data);
            return true;
        }

        public bool ValidaCliente(string? cliente)
        {
            ChecaClienteVazio(cliente);
            return true;
        }

        public bool ValidaEvento(DateTime? data, string? endereco)
        {
            var eventosExistentes = _repository.GetByDateAndAddress(data, endereco);
            ChecaSeEventoExiste(eventosExistentes);
            return true;
        }

        //Achei melhor separar instruções de alto nível das instruções de baixo nível

        static void ChecaDataVazia(DateTime? data)
        {
            if (!data.HasValue)
            {
                throw new InvalidDataException("A data do agendamento não pode ser vazia.");
            }
        }

        static void ChecaDataNoPassado(DateTime? data)
        {
            if (data <= DateTime.Now)
            {
                throw new InvalidDataException("Não é possível criar agendamento no passado.");
            }
        }

        static void ChecaClienteVazio(string? cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente))
            {
                throw new InvalidClientException("O nome do cliente não pode ser vazio.");
            }
        }

        static void ChecaSeEventoExiste(IEnumerable<Domain.Entidades.Agendamento> eventosExistentes)
        {
            if (eventosExistentes.Any())
            {
                throw new DuplicateEventException();
            }
        }

}

