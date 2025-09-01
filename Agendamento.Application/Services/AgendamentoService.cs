using Agendamento.Domain.Interfaces;

namespace Agendamento.Application.Services
{
    public class AgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public (bool IsSuccess, Domain.Entidades.Agendamento? Value, string? Error) Criar(AgendamentoDto dto)
        {
            if (dto.Data <= DateTime.Now)
                return (false, null, "Não é permitido criar agendamento no passado.");

            var agendamento = new Domain.Entidades.Agendamento
            {
                Cliente = dto.Cliente,
                Data = dto.Data,
                Endereco = dto.Endereco
            };

            _repository.Add(agendamento);
            return (true, agendamento, null);
        }

        public IEnumerable<Domain.Entidades.Agendamento> ObterTodos() => _repository.GetAll();
    }

    public class AgendamentoDto
    {
        public string Cliente { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Endereco { get; set; } = string.Empty;
    }
}
