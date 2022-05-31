using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sis_Controle_Viagens.Data.Migrations
{
    public partial class adicionado_usuario_tabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Pacotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Atendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Atendimentos");
        }
    }
}
