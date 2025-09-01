namespace Agendamento.Domain.Interfaces
{
    public interface IAgendamentoRepository
    {
        void Add(Entidades.Agendamento agendamento);
        IEnumerable<Entidades.Agendamento> GetAll();
    }
}
