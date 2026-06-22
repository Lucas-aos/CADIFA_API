namespace CadifaApi.Models;

public class Cadifa
{
    public int Id { get; set; }

    public string? Empresa { get; set; }

    public string? Insumo { get; set; }

    public string? Processo { get; set; }

    public string? Revisao { get; set; }

    public string? Data { get; set; }

    public string? Hash { get; set; }

    public DateTime DataColeta { get; set; }
}