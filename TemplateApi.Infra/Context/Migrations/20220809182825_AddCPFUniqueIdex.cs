using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    public partial class AddCPFUniqueIdex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "uk_person_cpf",
                table: "Person",
                column: "cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uk_person_cpf",
                table: "Person");
        }
    }
}
