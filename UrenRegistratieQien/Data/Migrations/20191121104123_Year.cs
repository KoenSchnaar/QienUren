using Microsoft.EntityFrameworkCore.Migrations;

namespace UrenRegistratieQien.Data.Migrations
{
    public partial class Year : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "DeclarationForms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "DeclarationForms");
        }
    }
}
