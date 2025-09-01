using Agendamento.Domain.Interfaces;

namespace Agendamento.Infrastructure
{
    public class InMemoryAgendamentoRepository : IAgendamentoRepository
    {
        private readonly List<Domain.Entidades.Agendamento> _storage = new();

        public void Add(Domain.Entidades.Agendamento agendamento) => _storage.Add(agendamento);

        public IEnumerable<Domain.Entidades.Agendamento> GetAll() => _storage;
    }
}
