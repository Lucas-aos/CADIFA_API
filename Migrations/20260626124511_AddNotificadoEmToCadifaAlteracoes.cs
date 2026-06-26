using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CadifaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificadoEmToCadifaAlteracoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cadifa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    empresa = table.Column<string>(type: "text", nullable: true),
                    insumo = table.Column<string>(type: "text", nullable: true),
                    processo = table.Column<string>(type: "text", nullable: true),
                    revisao = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<string>(type: "text", nullable: true),
                    hash = table.Column<string>(type: "text", nullable: true),
                    data_coleta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadifa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cadifa_alteracoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<string>(type: "text", nullable: true),
                    empresa = table.Column<string>(type: "text", nullable: true),
                    insumo = table.Column<string>(type: "text", nullable: true),
                    processo = table.Column<string>(type: "text", nullable: true),
                    revisao = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<string>(type: "text", nullable: true),
                    hash = table.Column<string>(type: "text", nullable: true),
                    data_deteccao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NotificadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadifa_alteracoes", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cadifa");

            migrationBuilder.DropTable(
                name: "cadifa_alteracoes");
        }
    }
}
