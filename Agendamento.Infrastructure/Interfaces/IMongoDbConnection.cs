using MongoDB.Driver;

namespace Agendamento.Infrastructure.Interfaces
{
    public interface IMongoDbConnection
    {
        public IMongoCollection<Domain.Entidades.Agendamento>? GetAgendamentosCollection();
    }
}
