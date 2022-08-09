using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    public partial class AddCPFCollumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sur_name",
                table: "Person",
                newName: "surname");

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "Person",
                type: "nchar(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                defaultValue: "00000000000");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cpf",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Person",
                newName: "sur_name");
        }
    }
}
