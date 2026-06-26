using CadifaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadifaApi.DTOs;
using CadifaApi.Services;

namespace CadifaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlteracoesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly NotificationService _notificationService;

    public AlteracoesController(AppDbContext context, NotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? tipo,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 100
    )
    {
        var query = _context.CadifaAlteracoes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            query = query.Where(x =>
                x.Tipo != null &&
                x.Tipo == tipo.ToUpper()
            );
        }

        var totalEncontrado = await query.CountAsync();

        var dados = await query
            .OrderByDescending(x => x.DataDeteccao)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            totalEncontrado,
            paginaAtual = page,
            tamanhoPagina = pageSize,
            dados
        });
    }
    [HttpGet("notificacao")]
    public async Task<ActionResult<AlteracoesNotificacaoResponse>> GetNotificacao()
    {

        var response = await _notificationService.ObterNotificacaoEmailAsync();

    return Ok(response);
    }
    [HttpGet("notificacao/detalhes")]
    public async Task<ActionResult<AlteracoesNotificacaoResponse>> GetNotificacaoDetalhes()
    {
        var response = await _notificationService.ObterAlteracoesPendentesAsync();
        return Ok(response);
    }
    [HttpPost("marcar-notificadas")]
    public async Task<IActionResult> MarcarComoNotificadas([FromBody] MarcarNotificadasRequest request)
    {

        var resultado = await _notificationService.MarcarComoNotificadasAsync(request.Ids);

        return Ok(resultado);
    }

}