using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrecaoDaTabelaEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE");

            migrationBuilder.AddColumn<string>(
                name: "DESCRICAO",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DESCRICAO",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }
    }
}
