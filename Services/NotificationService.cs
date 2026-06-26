using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadifaApi.Data;
using CadifaApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CadifaApi.Services;

public class NotificationService
{
    private readonly AppDbContext _context;
    private readonly EmailTemplateService _emailTemplateService;

    public NotificationService(
        AppDbContext context,
        EmailTemplateService emailTemplateService)
    {
        _context = context;
        _emailTemplateService = emailTemplateService;
    }

    public async Task<AlteracoesNotificacaoResponse> ObterAlteracoesPendentesAsync()
    {
        var alteracoes = await _context.CadifaAlteracoes
            .Where(a => a.NotificadoEm == null)
            .OrderByDescending(a => a.DataDeteccao)
            .Select(a => new AlteracaoNotificacaoItem
            {
                Id = a.Id,
                Tipo = a.Tipo ?? string.Empty,
                Empresa = a.Empresa ?? string.Empty,
                Insumo = a.Insumo ?? string.Empty,
                Processo = a.Processo ?? string.Empty,
                Revisao = a.Revisao,
                DataRegistro = a.DataDeteccao
            })
            .ToListAsync();

        var totalInclusoes = alteracoes.Count(a => a.Tipo.ToLower() == "inclusao");
        var totalExclusoes = alteracoes.Count(a => a.Tipo.ToLower() == "exclusao");

        return new AlteracoesNotificacaoResponse
        {
            PossuiAlteracoes = alteracoes.Any(),
            TotalInclusoes = totalInclusoes,
            TotalExclusoes = totalExclusoes,
            DataConsulta = DateTime.UtcNow,
            Ids = alteracoes.Select(a => a.Id).ToList(),
            Alteracoes = alteracoes
        };
    }

    public async Task<NotificacaoEmailResponse> ObterNotificacaoEmailAsync()
    {
        var dados = await ObterAlteracoesPendentesAsync();

        var totalAlteracoes = dados.Ids.Count;

        return new NotificacaoEmailResponse
        {
            PossuiAlteracoes = dados.PossuiAlteracoes,
            TotalInclusoes = dados.TotalInclusoes,
            TotalExclusoes = dados.TotalExclusoes,
            DataConsulta = dados.DataConsulta,
            Ids = dados.Ids,
            Assunto = totalAlteracoes > 0
                ? $"CADIFA - {totalAlteracoes} alteração(ões) detectada(s)"
                : "CADIFA - Nenhuma alteração detectada",
            Html = dados.PossuiAlteracoes
                ? _emailTemplateService.GerarHtml(
                    dados.Alteracoes,
                    dados.TotalInclusoes,
                    dados.TotalExclusoes,
                    dados.DataConsulta)
                : string.Empty
        };
    }

    public async Task<object> MarcarComoNotificadasAsync(List<int> ids)
    {
        if (ids == null || !ids.Any())
        {
            return new
            {
                sucesso = false,
                mensagem = "Nenhum ID informado.",
                total = 0
            };
        }

        var alteracoes = await _context.CadifaAlteracoes
            .Where(a => ids.Contains(a.Id) && a.NotificadoEm == null)
            .ToListAsync();

        if (!alteracoes.Any())
        {
            return new
            {
                sucesso = true,
                mensagem = "Nenhuma alteração pendente encontrada para os IDs informados.",
                total = 0
            };
        }

        var dataNotificacao = DateTime.UtcNow;

        foreach (var alteracao in alteracoes)
        {
            alteracao.NotificadoEm = dataNotificacao;
        }

        await _context.SaveChangesAsync();

        return new
        {
            sucesso = true,
            mensagem = "Alterações marcadas como notificadas com sucesso.",
            total = alteracoes.Count,
            ids = alteracoes.Select(a => a.Id).ToList(),
            dataNotificacao
        };
    }
}
