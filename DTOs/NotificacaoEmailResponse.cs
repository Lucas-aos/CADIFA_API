using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadifaApi.DTOs;

public class NotificacaoEmailResponse
{
    public bool PossuiAlteracoes { get; set; }
    public string Assunto { get; set; } = string.Empty;
    public string Html { get; set; } = string.Empty;
    public List<int> Ids { get; set; } = new();
    public int TotalInclusoes { get; set; }
    public int TotalExclusoes { get; set; }
    public DateTime DataConsulta { get; set; }
}
