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
}
