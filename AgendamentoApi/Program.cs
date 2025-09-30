using Agendamento.Application.Services;
using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Services;
using Agendamento.Infrastructure;
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
builder.Services.AddSingleton<IAgendamentoRepository, InMemoryAgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoValidator, AgendamentoValidator>();
builder.Services.AddScoped<AgendamentoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agendamento API V1");
});

app.MapControllers();

app.Run();