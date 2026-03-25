using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCF_CAMPANHA_SUPERVISORES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CF_CAMPANHA_RCA_SCORE",
                schema: "HOMOLOGA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USUARI_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PONTOS = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NOME_GERENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_CAMPANHA_RCA_SCORE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CF_CAMPANHA_RCA_SCORE_CF_CAMPANHA_USUARIO_USUARI_ID",
                        column: x => x.USUARI_ID,
                        principalSchema: "HOMOLOGA",
                        principalTable: "CF_CAMPANHA_USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CF_CAMPANHA_SUPERVISORES",
                schema: "HOMOLOGA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODSUPERVISOR = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_CAMPANHA_SUPERVISORES", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_SUPERVISORES",
                columns: new[] { "ID", "CODSUPERVISOR", "NOME" },
                values: new object[,]
                {
                    { 1, 25, "Neto" },
                    { 2, 3, "Bruno" },
                    { 3, 11, "Bruno" },
                    { 4, 12, "Bruno" },
                    { 5, 13, "Bruno" },
                    { 6, 33, "Bruno" },
                    { 7, 27, "Rodrigo" },
                    { 8, 30, "Leandro" },
                    { 9, 34, "Leandro" },
                    { 10, 14, "Zé Rubens" },
                    { 11, 15, "Zé Rubens" },
                    { 12, 28, "Zé Rubens" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CF_CAMPANHA_RCA_SCORE_USUARI_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE",
                column: "USUARI_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CF_CAMPANHA_RCA_SCORE",
                schema: "HOMOLOGA");

            migrationBuilder.DropTable(
                name: "CF_CAMPANHA_SUPERVISORES",
                schema: "HOMOLOGA");
        }
    }
}
