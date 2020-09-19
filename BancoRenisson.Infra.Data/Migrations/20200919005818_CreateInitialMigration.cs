using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoRenisson.Infra.Data.Migrations
{
    public partial class CreateInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_COR_ContaCorrente",
                columns: table => new
                {
                    cor_id = table.Column<Guid>(nullable: false),
                    cor_dataCriacao = table.Column<DateTime>(nullable: false),
                    cor_dataUltimaAtualizacao = table.Column<DateTime>(nullable: false),
                    cor_numero = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cor_nomeTitular = table.Column<string>(nullable: false),
                    cor_valorDisponivel = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_COR_ContaCorrente", x => x.cor_id);
                    table.UniqueConstraint("AK_tbl_COR_ContaCorrente_cor_numero", x => x.cor_numero);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MOV_Movimentacao",
                columns: table => new
                {
                    mov_id = table.Column<Guid>(nullable: false),
                    mov_dataCriacao = table.Column<DateTime>(nullable: false),
                    mov_dataUltimaAtualizacao = table.Column<DateTime>(nullable: false),
                    mov_valor = table.Column<decimal>(nullable: false),
                    mov_descricao = table.Column<string>(nullable: true),
                    cor_id = table.Column<Guid>(nullable: false),
                    mov_tipo_operacao = table.Column<int>(nullable: false),
                    mov_tipo_operacao_descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MOV_Movimentacao", x => x.mov_id);
                    table.ForeignKey(
                        name: "FK_tbl_MOV_Movimentacao_tbl_COR_ContaCorrente_cor_id",
                        column: x => x.cor_id,
                        principalTable: "tbl_COR_ContaCorrente",
                        principalColumn: "cor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MOV_Movimentacao_cor_id",
                table: "tbl_MOV_Movimentacao",
                column: "cor_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MOV_Movimentacao");

            migrationBuilder.DropTable(
                name: "tbl_COR_ContaCorrente");
        }
    }
}
