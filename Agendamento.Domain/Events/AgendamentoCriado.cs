namespace Agendamento.Domain.Events;

    public record AgendamentoCriado(Guid Id, string Cliente, DateTime Data, string Endereco);

