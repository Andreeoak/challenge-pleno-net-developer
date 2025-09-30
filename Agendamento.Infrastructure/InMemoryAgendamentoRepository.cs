using Agendamento.Domain.Interfaces;

namespace Agendamento.Infrastructure
{
    public class InMemoryAgendamentoRepository : IAgendamentoRepository
    {
        private readonly List<Domain.Entidades.Agendamento> _storage = new();

        public void Add(Domain.Entidades.Agendamento agendamento) => _storage.Add(agendamento);

        public IEnumerable<Domain.Entidades.Agendamento> GetAll() => _storage;

        public IEnumerable<Domain.Entidades.Agendamento> GetByDateAndAddress(DateTime? date, string? address) =>
            _storage.Where(row => row.Data == date && row.Endereco.Equals(address, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Domain.Entidades.Agendamento> GetById(Guid? id) 
        {
            if (!id.HasValue)
                return Enumerable.Empty<Domain.Entidades.Agendamento>();

            return _storage.Where(row => row.Id == id.Value);
        }
    }
}
