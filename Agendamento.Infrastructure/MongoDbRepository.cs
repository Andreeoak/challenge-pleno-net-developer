using Agendamento.Domain.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;
using Agendamento.Infrastructure.Interfaces;

namespace Agendamento.Infrastructure;

public class MongoDbRepository : IAgendamentoRepository
{
    private readonly IMongoCollection<Domain.Entidades.Agendamento?>? _agendamentosCollection;

    public MongoDbRepository(IMongoDbConnection connection)
    {
        _agendamentosCollection = connection.GetAgendamentosCollection();
    }

    public void Add(Domain.Entidades.Agendamento agendamento)
    {
        GetCollection().InsertOne(agendamento);
    }

    public IEnumerable<Domain.Entidades.Agendamento> GetAll()
    {
        return GetCollection().Find(new BsonDocument()).ToList()!;
    }

    public IEnumerable<Domain.Entidades.Agendamento> GetById(Guid? id)
    {
        if (!id.HasValue)
            return Enumerable.Empty<Domain.Entidades.Agendamento>();

        var filter = Builders<Domain.Entidades.Agendamento>.Filter.Eq(a => a.Id, id.Value);
        return GetCollection().Find(filter).ToList()!;
    }

    public IEnumerable<Domain.Entidades.Agendamento> GetByDateAndAddress(DateTime? date, string? address)
    {
        var filterBuilder = Builders<Domain.Entidades.Agendamento>.Filter;
        var filter = filterBuilder.Empty;
        
        filter &= filterBuilder.Eq(a => a.Data, date);
        filter &= filterBuilder.Eq(a => a.Endereco, address);

        return GetCollection().Find(filter).ToList()!;
    }

    // Centralizando a validação
    private IMongoCollection<Domain.Entidades.Agendamento?> GetCollection()
    {
        return _agendamentosCollection
            ?? throw new InvalidOperationException("MongoDB collection is not available.");
    }
}
