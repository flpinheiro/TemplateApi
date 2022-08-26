using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddIAuditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Update_date",
                table: "Person",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "Person",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 23, 16, 58, 53, 298, DateTimeKind.Local).AddTicks(9499));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Update_date",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "Person");
        }
    }
}
