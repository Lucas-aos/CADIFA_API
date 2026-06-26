using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadifaApi.DTOs;


namespace CadifaApi.Services;

public class EmailTemplateService
{
    public string GerarHtml(
        List<AlteracaoNotificacaoItem> alteracoes,
        int totalInclusoes,
        int totalExclusoes,
        DateTime dataConsulta)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<html>");
        sb.AppendLine("<body style='font-family: Arial, sans-serif;'>");

        sb.AppendLine("<h2>Monitoramento CADIFA - ANVISA</h2>");
        sb.AppendLine("<p>Foram detectadas alterações na base CADIFA.</p>");

        sb.AppendLine("<p>");
        sb.AppendLine($"<b>Inclusões:</b> {totalInclusoes}<br>");
        sb.AppendLine($"<b>Exclusões:</b> {totalExclusoes}<br>");
        sb.AppendLine($"<b>Data da consulta:</b> {dataConsulta:dd/MM/yyyy HH:mm}");
        sb.AppendLine("</p>");

        sb.AppendLine("<table border='1' cellpadding='6' cellspacing='0' style='border-collapse: collapse;'>");
        sb.AppendLine("<thead>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<th>Tipo</th>");
        sb.AppendLine("<th>Empresa</th>");
        sb.AppendLine("<th>Insumo</th>");
        sb.AppendLine("<th>Processo</th>");
        sb.AppendLine("<th>Revisão</th>");
        sb.AppendLine("<th>Data Detecção</th>");
        sb.AppendLine("</tr>");
        sb.AppendLine("</thead>");
        sb.AppendLine("<tbody>");

        foreach (var item in alteracoes)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine($"<td>{item.Tipo}</td>");
            sb.AppendLine($"<td>{item.Empresa}</td>");
            sb.AppendLine($"<td>{item.Insumo}</td>");
            sb.AppendLine($"<td>{item.Processo}</td>");
            sb.AppendLine($"<td>{item.Revisao}</td>");
            sb.AppendLine($"<td>{item.DataRegistro:dd/MM/yyyy HH:mm}</td>");
            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</tbody>");
        sb.AppendLine("</table>");

        sb.AppendLine("<p>");
        sb.AppendLine("<a href='https://cadifa-api.onrender.com/swagger'>Abrir CADIFA API</a>");
        sb.AppendLine("</p>");

        sb.AppendLine("<p style='font-size: 12px; color: #666;'>");
        sb.AppendLine("E-mail gerado automaticamente pelo monitor CADIFA.");
        sb.AppendLine("</p>");

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }
}