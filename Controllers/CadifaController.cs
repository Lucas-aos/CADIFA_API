using CadifaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadifaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadifaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CadifaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? empresa,
        [FromQuery] string? processo,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 100
    )
    {
        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 100;

        if (pageSize > 500)
            pageSize = 500;

        var query = _context.Cadifa.AsQueryable();

        if (!string.IsNullOrWhiteSpace(empresa))
        {
            query = query.Where(x =>
                x.Empresa != null &&
                EF.Functions.ILike(x.Empresa, $"%{empresa}%")
            );
        }

        if (!string.IsNullOrWhiteSpace(processo))
        {
            query = query.Where(x =>
                x.Processo != null &&
                EF.Functions.ILike(x.Processo, $"%{processo}%")
            );
        }

        var totalEncontrado = await query.CountAsync();

        var dados = await query
            .OrderBy(x => x.Empresa)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            totalEncontrado,
            paginaAtual = page,
            tamanhoPagina = pageSize,
            totalRetornado = dados.Count,
            dados
        });
    }
}