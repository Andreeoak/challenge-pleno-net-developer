using Agendamento.Application.Services;
using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Services;
using Agendamento.Infrastructure;
using Agendamento.Infrastructure.Debug;
using Agendamento.Infrastructure.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agendamento API", Version = "v1" });
});

// Registrando camadas
builder.Services.AddSingleton<IMongoDbConnection, MongoDbConnection>();
builder.Services.AddSingleton<IAgendamentoRepository, MongoDbRepository>();
builder.Services.AddSingleton<IMessageBus, InMemoryMessageBus>();
builder.Services.AddScoped<IAgendamentoValidator, AgendamentoValidator>();
builder.Services.AddScoped<AgendamentoService>();

builder.Services.AddSingleton<ConsoleEventHandler>();

var app = builder.Build();

app.Services.GetRequiredService<ConsoleEventHandler>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agendamento API V1");
});

app.MapControllers();

app.Run();