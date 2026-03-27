using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AlterTables_RemovidoCampos_AlteradoTipoDeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_CAMPANHA_RCA_SCORE_CF_CAMPANHA_USUARIO_USUARI_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE");

            migrationBuilder.DropIndex(
                name: "IX_CF_CAMPANHA_RCA_SCORE_USUARI_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE");

            migrationBuilder.AlterColumn<decimal>(
                name: "PONTOS",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PONTOS",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.CreateIndex(
                name: "IX_CF_CAMPANHA_RCA_SCORE_USUARI_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE",
                column: "USUARI_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CF_CAMPANHA_RCA_SCORE_CF_CAMPANHA_USUARIO_USUARI_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_RCA_SCORE",
                column: "USUARI_ID",
                principalSchema: "HOMOLOGA",
                principalTable: "CF_CAMPANHA_USUARIO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
