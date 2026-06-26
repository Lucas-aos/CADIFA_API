using Microsoft.EntityFrameworkCore;
namespace CadifaApi.DTOs;

public class AlteracoesNotificacaoResponse
{
    public bool PossuiAlteracoes { get; set; }
    public int TotalInclusoes { get; set; }
    public int TotalExclusoes { get; set; }
    public DateTime DataConsulta { get; set; }
    public List<int> Ids { get; set; } = new();
    public List<AlteracaoNotificacaoItem> Alteracoes { get; set; } = new();
}

public class AlteracaoNotificacaoItem
{
    public int Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
    public string Insumo { get; set; } = string.Empty;
    public string Processo { get; set; } = string.Empty;
    public string? Revisao { get; set; }
    public DateTime DataRegistro { get; set; }
}
public class MarcarNotificadasRequest
{
    public List<int> Ids { get; set; } = new();
}