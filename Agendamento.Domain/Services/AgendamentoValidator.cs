using Agendamento.Domain.Entidades;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Domain.Services;

    public class AgendamentoValidator: IAgendamentoValidator
    {
        private IAgendamentoRepository _repository { get; init; }
        
        public AgendamentoValidator(IAgendamentoRepository repository)
        {
            _repository = repository;
        }
        
        public void ValidaAgendamento(Entidades.Agendamento agendamento)
        {
            this.ValidaData(agendamento.Data);
            this.ValidaCliente(agendamento.Cliente);
            this.ValidaEvento(agendamento.Data, agendamento.Endereco);

        }


        private bool ValidaData(DateTime? data)
        {
            ChecaDataVazia(data);
            ChecaDataNoPassado(data);
            return true;
        }

        private bool ValidaCliente(string? cliente)
        {
            ChecaClienteVazio(cliente);
            return true;
        }

        private bool ValidaEvento(DateTime? data, string? endereco)
        {
            var eventosExistentes = _repository.GetByDateAndAddress(data, endereco);
            ChecaSeEventoExiste(eventosExistentes);
            return true;
        }

        //Achei melhor separar instruções de alto nível das instruções de baixo nível

        private static void ChecaDataVazia(DateTime? data)
        {
            if (!data.HasValue)
            {
                throw new InvalidDataException("A data do agendamento não pode ser vazia.");
            }
        }

        private static void ChecaDataNoPassado(DateTime? data)
        {
            if (data <= DateTime.Now)
            {
                throw new InvalidDataException("Não é possível criar agendamento no passado.");
            }
        }

        private static void ChecaClienteVazio(string? cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente))
            {
                throw new InvalidClientException("O nome do cliente não pode ser vazio.");
            }
        }

        private static void ChecaSeEventoExiste(IEnumerable<Domain.Entidades.Agendamento> eventosExistentes)
        {
            if (eventosExistentes.Any())
            {
                throw new DuplicateEventException();
            }
        }

}

