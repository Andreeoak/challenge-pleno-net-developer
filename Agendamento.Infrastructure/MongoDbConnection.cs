using Agendamento.Infrastructure.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Agendamento.Infrastructure;

public class MongoDbConnection : IMongoDbConnection
{
    private readonly IConfiguration _configuration;
    private const string DatabaseName = "Library";
    private const string CollectionName = "Agendamentos";

    public MongoDbConnection(IConfiguration configuration)
    {

        _configuration = configuration;
    }

    public IMongoCollection<Domain.Entidades.Agendamento?>? GetAgendamentosCollection()
    {
        var database = GetDatabase();
        if (database is null) return null;

        return database.GetCollection<Domain.Entidades.Agendamento?>(CollectionName);
    }

    private IMongoDatabase? GetDatabase()
    {
        var (username, password) = GetCredentials();

        if (!ValidateCredentials(username, password))
        {
            Console.WriteLine("MongoDB credentials are invalid or missing.");
            return null;
        }

        var client = CreateClient(username!, password!);
        return FetchDatabase(client, DatabaseName);
    }

    
    private (string? Username, string? Password) GetCredentials()
    {
        string? username = _configuration["MONGO_DB_USERNAME"]; 
        string? password = _configuration["MONGO_DB_PASSWORD"];
        return (username, password);
    }

    
    private bool ValidateCredentials(string? username, string? password)
    {
        return !(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password));
    }

    
    private MongoClient CreateClient(string username, string password)
    {
        string connectionUri =
            $"mongodb+srv://{username}:{password}@cluster0.iyivjfm.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        return new MongoClient(settings);
    }

    
    private IMongoDatabase? FetchDatabase(MongoClient client, string databaseName)
    {
        try
        {
            var database = client.GetDatabase(databaseName);
            Console.WriteLine("Pinged your deployment. Successfully connected to MongoDB!");
            return database;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MongoDB connection failed: {ex.Message}");
            return null;
        }
    }
}
