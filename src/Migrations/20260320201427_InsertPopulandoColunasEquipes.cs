using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campaign.API.Migrations
{
    public partial class InsertPopulandoColunasEquipes : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CF_CAMPANHA_EQUIPE",
                columns: ["ID", "NOME", "DESCRICAO"],
                values: [1, "F-117 BLACK JET", "RCAs que faturam mais de R$700,00 mil."]);

            migrationBuilder.InsertData(
                table: "CF_CAMPANHA_EQUIPE",
                columns: ["ID", "NOME", "DESCRICAO"],
                values: [2, "F-22 Raptor", "RCAs que faturam de R$400,00 a R$699 mil."]);

            migrationBuilder.InsertData(
                table: "CF_CAMPANHA_EQUIPE",
                columns: ["ID", "NOME", "DESCRICAO"],
                values: [3, "F-35 Lightning", "RCAs que faturam de R$200,00 a R$399,00 mil."]);

            migrationBuilder.InsertData(
                table: "CF_CAMPANHA_EQUIPE",
                columns: ["ID", "NOME", "DESCRICAO"],
                values: [4, "F-14 Tomcat", "RCAs quefaturam de R$100 a R$199 mil."]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "CF_CAMPANHA_EQUIPE", "ID", keyValue: 1);
            migrationBuilder.DeleteData(table: "CF_CAMPANHA_EQUIPE", "ID", keyValue: 2);
            migrationBuilder.DeleteData(table: "CF_CAMPANHA_EQUIPE", "ID", keyValue: 3);
            migrationBuilder.DeleteData(table: "CF_CAMPANHA_EQUIPE", "ID", keyValue: 4);
        }
    }
}