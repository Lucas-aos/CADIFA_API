using CadifaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadifaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly AppDbContext _context;

    public StatusController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var totalCadifa = await _context.Cadifa.CountAsync();
        var totalAlteracoes = await _context.CadifaAlteracoes.CountAsync();

        return Ok(new
        {
            api = "online",
            banco = "conectado",
            totalCadifa,
            totalAlteracoes,
            dataConsulta = DateTime.Now
        });
    }

    [HttpGet("resumo")]
    public async Task<IActionResult> Resumo()
    {
        var totalCadifa = await _context.Cadifa.CountAsync();

        var totalInclusoes = await _context.CadifaAlteracoes
            .CountAsync(x => x.Tipo == "INCLUSAO");

        var totalExclusoes = await _context.CadifaAlteracoes
            .CountAsync(x => x.Tipo == "EXCLUSAO");

        DateTime? ultimaAlteracao = await _context.CadifaAlteracoes
        .OrderByDescending(x => x.DataDeteccao)
        .Select(x => (DateTime?)x.DataDeteccao)
        .FirstOrDefaultAsync();

        return Ok(new
        {
            totalCadifa,
            totalInclusoes,
            totalExclusoes,
            totalAlteracoes = totalInclusoes + totalExclusoes,
            ultimaAlteracao,
            dataConsulta = DateTime.Now
        });
    }
}