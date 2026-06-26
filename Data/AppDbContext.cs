using Microsoft.EntityFrameworkCore;
using CadifaApi.Models;

namespace CadifaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cadifa> Cadifa => Set<Cadifa>();
    public DbSet<CadifaAlteracao> CadifaAlteracoes => Set<CadifaAlteracao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cadifa>(entity =>
        {
            entity.ToTable("cadifa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Empresa).HasColumnName("empresa");
            entity.Property(e => e.Insumo).HasColumnName("insumo");
            entity.Property(e => e.Processo).HasColumnName("processo");
            entity.Property(e => e.Revisao).HasColumnName("revisao");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.DataColeta).HasColumnName("data_coleta");
        });

        modelBuilder.Entity<CadifaAlteracao>(entity =>
        {
            entity.ToTable("cadifa_alteracoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.Empresa).HasColumnName("empresa");
            entity.Property(e => e.Insumo).HasColumnName("insumo");
            entity.Property(e => e.Processo).HasColumnName("processo");
            entity.Property(e => e.Revisao).HasColumnName("revisao");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.DataDeteccao).HasColumnName("data_deteccao");
            entity.Property(e => e.NotificadoEm).HasColumnName("notificado_em");
        });
    }
}