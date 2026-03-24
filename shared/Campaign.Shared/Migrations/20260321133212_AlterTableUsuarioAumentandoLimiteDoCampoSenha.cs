using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableUsuarioAumentandoLimiteDoCampoSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SENHA",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SENHA",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200);
        }
    }
}
