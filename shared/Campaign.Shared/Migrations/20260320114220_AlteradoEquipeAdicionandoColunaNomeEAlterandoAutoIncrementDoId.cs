using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoEquipeAdicionandoColunaNomeEAlterandoAutoIncrementDoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_EQUIPE",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");
        }
    }
}
