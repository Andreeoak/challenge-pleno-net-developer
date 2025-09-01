using Agendamento.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendamentosController : ControllerBase
{
    private readonly AgendamentoService _service;

    public AgendamentosController(AgendamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_service.ObterTodos());

    [HttpPost]
    public IActionResult Post([FromBody] AgendamentoDto dto)
    {
        var result = _service.Criar(dto);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
