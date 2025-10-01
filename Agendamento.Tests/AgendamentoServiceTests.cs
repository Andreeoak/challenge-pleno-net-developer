using Agendamento.Application.Services;
using Agendamento.Domain.Services;
using Agendamento.Infrastructure;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Tests
{
    public class AgendamentoServiceTests
    {
        private AgendamentoService _service;
        private readonly IAgendamentoRepository _repo;
        private readonly IAgendamentoValidator _validator;
        private readonly IMessageBus _bus;
        public AgendamentoServiceTests() 
        {
            _repo = new InMemoryAgendamentoRepository();
            _validator = new AgendamentoValidator(_repo);
            _bus = new InMemoryMessageBus();
            _service = new AgendamentoService(_repo, _validator, _bus);          
        }


        [Fact]
        public void NaoDevePermitirAgendamentoNoPassado()
        {
            var dto = new AgendamentoDto
            {
                Cliente = "João",
                Data = DateTime.Now.AddDays(-1),
                Endereco = "Rua A"
            };

            var result = _service.Criar(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Não é possível criar agendamento no passado.", result.Error);
        }

        [Fact]
        public void NaoDevePermitirAgendamentoDuplicado()
        {
            DateTime mesmaData = DateTime.Now.AddDays(+1);
            var dto = new AgendamentoDto
            {
                Cliente = "João",
                Data = mesmaData,
                Endereco = "Rua A"
            };

            var result = _service.Criar(dto);

            var dto2 = new AgendamentoDto
            {
                Cliente = "João",
                Data = mesmaData,
                Endereco = "Rua A"
            };

            var result2 = _service.Criar(dto2);

            Assert.False(result2.IsSuccess);
            Assert.Equal("Já existe um agendamento para esta data e endereço.", result2.Error);
        }

        [Fact]
        public void NaoDevePermitirAgendamentoComClienteVazio()
        {
            var dto = new AgendamentoDto
            {
                Cliente = "       ",
                Data = DateTime.Now.AddDays(+1),
                Endereco = "Rua A"
            };

            var result = _service.Criar(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("O nome do cliente não pode ser vazio.", result.Error);

        }

        [Fact]
        public void NaoDevePermitirAgendamentoComClienteNull()
        {
            var dto = new AgendamentoDto
            {
                Cliente = null,
                Data = DateTime.Now.AddDays(+1),
                Endereco = "Rua A"
            };

            var result = _service.Criar(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("O nome do cliente não pode ser vazio.", result.Error);

        }

        [Fact]
        public void DevePermitirAgendamentoValido()
        {
            var dto = new AgendamentoDto
            {
                Cliente = "Maria",
                Data = DateTime.Now.AddDays(+1),
                Endereco = "Rua B"
            };

            var result = _service.Criar(dto);
            var agendamentoCriado = result.Value;

            Assert.True(result.IsSuccess);
        }

        public void DeveEncontrarNovoAgendamentoPeloId()
        {
            var dto = new AgendamentoDto
            {
                Cliente = "Maria",
                Data = DateTime.Now.AddDays(+1),
                Endereco = "Rua B"
            };

            var result = _service.Criar(dto);
            var agendamentoCriado = result.Value;

            var retorno = _service.ObterPeloId(agendamentoCriado.Id);

            Assert.NotNull(retorno);
            Assert.Single(retorno);
            Assert.Equal("Maria", retorno.First().Cliente);
        }

        [Fact]
        public void NaoDeveEncontrarAgendamentoComIdInexistente()
        {
            var retorno = _service.ObterPeloId(Guid.NewGuid());

            Assert.NotNull(retorno);
            Assert.Empty(retorno);
        }

        [Fact]
        public void NaoDeveEncontrarAgendamentoComIdNull()
        {
            var retorno = _service.ObterPeloId(null);

            Assert.NotNull(retorno);
            Assert.Empty(retorno);
        }
    }
}