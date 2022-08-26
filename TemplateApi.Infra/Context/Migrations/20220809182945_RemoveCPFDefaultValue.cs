using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class RemoveCPFDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "Person",
                type: "char(11)",
                unicode: false,
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(11)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 11,
                oldDefaultValue: "00000000000");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "Person",
                type: "char(11)",
                unicode: false,
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                defaultValue: "00000000000",
                oldClrType: typeof(string),
                oldType: "char(11)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 11);
        }
    }
}
