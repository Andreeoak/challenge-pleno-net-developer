using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Services;
using Agendamento.Domain.Events;

namespace Agendamento.Application.Services;

    public class AgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        private readonly IAgendamentoValidator _validator;

        private readonly IMessageBus _bus;

        public AgendamentoService(IAgendamentoRepository repository, IAgendamentoValidator validator, IMessageBus bus)
        {
            _repository = repository;
            _validator = validator;
            _bus = bus;
        }

        public (bool IsSuccess, Domain.Entidades.Agendamento? Value, string? Error) Criar(AgendamentoDto dto)
        {
            try
            { 
                var agendamento = new Domain.Entidades.Agendamento
                {
                    Cliente = dto.Cliente,
                    Data = dto.Data,
                    Endereco = dto.Endereco
                };

                _validator.ValidaAgendamento(agendamento);

                _repository.Add(agendamento);
                _bus.Publish(new AgendamentoCriado(agendamento.Id, agendamento.Cliente, agendamento.Data, agendamento.Endereco));
               
                return (true, agendamento, null);

            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
            
        }

        public IEnumerable<Domain.Entidades.Agendamento> ObterTodos() => _repository.GetAll();

        public IEnumerable<Domain.Entidades.Agendamento> ObterPeloId(Guid? id)
        {
            return _repository.GetById(id);
        }
    }

    public class AgendamentoDto
    {
        public string Cliente { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Endereco { get; set; } = string.Empty;
    }


