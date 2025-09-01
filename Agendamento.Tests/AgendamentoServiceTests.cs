using Agendamento.Application.Services;
using Agendamento.Infrastructure;

namespace Agendamento.Tests
{
    public class AgendamentoServiceTests
    {
        [Fact]
        public void NaoDevePermitirAgendamentoNoPassado()
        {
            var repo = new InMemoryAgendamentoRepository();
            var service = new AgendamentoService(repo);

            var dto = new AgendamentoDto
            {
                Cliente = "João",
                Data = DateTime.Now.AddDays(-1),
                Endereco = "Rua A"
            };

            var result = service.Criar(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Não é permitido criar agendamento no passado.", result.Error);
        }
    }
}