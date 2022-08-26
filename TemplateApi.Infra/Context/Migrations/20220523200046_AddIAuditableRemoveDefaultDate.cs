using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddIAuditableRemoveDefaultDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Update_date",
                table: "Person",
                newName: "update_date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "Person",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 23, 16, 58, 53, 298, DateTimeKind.Local).AddTicks(9499));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "update_date",
                table: "Person",
                newName: "Update_date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "Person",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 23, 16, 58, 53, 298, DateTimeKind.Local).AddTicks(9499),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
