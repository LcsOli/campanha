using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableUsuarioRenomeadoColunaSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SENHA",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                newName: "SENHA_CRIPTOGRAFADA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SENHA_CRIPTOGRAFADA",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                newName: "SENHA");
        }
    }
}
