using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    public partial class AddIsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "update_date",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "create_date",
                table: "Person",
                newName: "last_update_date");

            migrationBuilder.AddColumn<bool>(
                name: "is_updated",
                table: "Person",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_updated",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "last_update_date",
                table: "Person",
                newName: "create_date");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_date",
                table: "Person",
                type: "datetime2",
                nullable: true);
        }
    }
}
