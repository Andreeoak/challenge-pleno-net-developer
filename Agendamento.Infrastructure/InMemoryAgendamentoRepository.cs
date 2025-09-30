using Agendamento.Domain.Interfaces;

namespace Agendamento.Infrastructure
{
    public class InMemoryAgendamentoRepository : IAgendamentoRepository
    {
        private readonly List<Domain.Entidades.Agendamento> _storage = new();

        public void Add(Domain.Entidades.Agendamento agendamento) => _storage.Add(agendamento);

        public IEnumerable<Domain.Entidades.Agendamento> GetAll() => _storage;

        public IEnumerable<Domain.Entidades.Agendamento> GetByDateAndAddress(DateTime? date, string? address) =>
            _storage.Where(a => a.Data == date && a.Endereco.Equals(address, StringComparison.OrdinalIgnoreCase));
    }
}
