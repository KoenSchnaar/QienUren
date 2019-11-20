using Microsoft.EntityFrameworkCore.Migrations;

namespace UrenRegistratieQien.Data.Migrations
{
    public partial class AdressToevoegingen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Residence",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZIPCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Residence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZIPCode",
                table: "AspNetUsers");
        }
    }
}
