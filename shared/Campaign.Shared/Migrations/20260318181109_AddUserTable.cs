using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HOMOLOGA");

            migrationBuilder.CreateTable(
                name: "CF_CAMPANHA_EQUIPE",
                schema: "HOMOLOGA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_CAMPANHA_EQUIPE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CF_CAMPANHA_USUARIO",
                schema: "HOMOLOGA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EQUIPE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GERENTE_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ROLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ULTIMO_ACESSO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_CAMPANHA_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CF_CAMPANHA_USUARIO_CF_CAMPANHA_EQUIPE_EQUIPE_ID",
                        column: x => x.EQUIPE_ID,
                        principalSchema: "HOMOLOGA",
                        principalTable: "CF_CAMPANHA_EQUIPE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CF_CAMPANHA_USUARIO_CF_CAMPANHA_USUARIO_GERENTE_ID",
                        column: x => x.GERENTE_ID,
                        principalSchema: "HOMOLOGA",
                        principalTable: "CF_CAMPANHA_USUARIO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CF_CAMPANHA_USUARIO_EQUIPE_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                column: "EQUIPE_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CF_CAMPANHA_USUARIO_GERENTE_ID",
                schema: "HOMOLOGA",
                table: "CF_CAMPANHA_USUARIO",
                column: "GERENTE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CF_CAMPANHA_USUARIO",
                schema: "HOMOLOGA");

            migrationBuilder.DropTable(
                name: "CF_CAMPANHA_EQUIPE",
                schema: "HOMOLOGA");
        }
    }
}
