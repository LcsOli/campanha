using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePromotionReadDataHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CF_CAMPANHA_PROMOI_HISTORICO_LEITURA",
                schema: "HOMOLOGA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODPROMOCAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_LEITURA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_CAMPANHA_PROMOI_HISTORICO_LEITURA", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CF_CAMPANHA_PROMOI_HISTORICO_LEITURA",
                schema: "HOMOLOGA");
        }
    }
}
