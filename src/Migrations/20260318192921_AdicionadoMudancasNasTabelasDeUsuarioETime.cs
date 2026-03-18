using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoMudancasNasTabelasDeUsuarioETime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CF_CAMPANHA_USUARIO_GERENTE_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO");

            migrationBuilder.AlterColumn<string>(
                name: "SENHA",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.CreateIndex(
                name: "IX_CF_CAMPANHA_USUARIO_GERENTE_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                column: "GERENTE_ID",
                unique: true,
                filter: "\"GERENTE_ID\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CF_CAMPANHA_USUARIO_GERENTE_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO");

            migrationBuilder.AlterColumn<string>(
                name: "SENHA",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_CF_CAMPANHA_USUARIO_GERENTE_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                column: "GERENTE_ID");
        }
    }
}
