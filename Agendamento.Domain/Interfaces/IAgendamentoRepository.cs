namespace Agendamento.Domain.Interfaces
{
    public interface IAgendamentoRepository
    {
        void Add(Entidades.Agendamento agendamento);
        IEnumerable<Entidades.Agendamento> GetAll();
        IEnumerable<Entidades.Agendamento> GetByDateAndAddress(DateTime? date, string? address);
    }
}
