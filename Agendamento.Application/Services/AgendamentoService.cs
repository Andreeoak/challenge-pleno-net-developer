using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Agendamento.Application.Services;

    public class AgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        private IAgendamentoValidator _validator;

        public AgendamentoService(IAgendamentoRepository repository, IAgendamentoValidator validator)
        {
            _repository = repository;
            _validator = _validator ?? new AgendamentoValidator(_repository);
        }

        public (bool IsSuccess, Domain.Entidades.Agendamento? Value, string? Error) Criar(AgendamentoDto dto)
        {
            try
            {

                Console.WriteLine("Valor de data: " + dto.Data);
                Console.WriteLine($"Cliente recebido: {dto.Cliente}");
                Console.WriteLine("Evento: {0} - {1}", dto.Data, dto.Endereco);

                _validator.ValidaData(dto.Data);
                _validator.ValidaCliente(dto.Cliente);
                _validator.ValidaEvento(dto.Data, dto.Endereco);

                var agendamento = new Domain.Entidades.Agendamento
                {
                    Cliente = dto.Cliente,
                    Data = dto.Data,
                    Endereco = dto.Endereco
                };

                _repository.Add(agendamento);
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

