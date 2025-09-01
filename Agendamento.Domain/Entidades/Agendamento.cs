namespace Agendamento.Domain.Entidades
{
    public class Agendamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Cliente { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Endereco { get; set; } = string.Empty;
    }
}
