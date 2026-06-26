namespace CadifaApi.Models;

public class CadifaAlteracao
{
    public int Id { get; set; }

    public string? Tipo { get; set; }

    public string? Empresa { get; set; }

    public string? Insumo { get; set; }

    public string? Processo { get; set; }

    public string? Revisao { get; set; }

    public string? Data { get; set; }

    public string? Hash { get; set; }

    public DateTime DataDeteccao { get; set; }

    public DateTime? NotificadoEm { get; set; }
}