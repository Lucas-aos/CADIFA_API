using CadifaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadifaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlteracoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlteracoesController(AppDbContext context)
    {
        _context = context;
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
}