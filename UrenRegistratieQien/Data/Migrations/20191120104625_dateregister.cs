using Microsoft.EntityFrameworkCore.Migrations;

namespace UrenRegistratieQien.Data.Migrations
{
    public partial class dateregister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationForms_AspNetUsers_EmployeeId1",
                table: "DeclarationForms");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationForms_EmployeeId1",
                table: "DeclarationForms");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "DeclarationForms");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "DeclarationForms",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationForms_EmployeeId",
                table: "DeclarationForms",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationForms_AspNetUsers_EmployeeId",
                table: "DeclarationForms",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationForms_AspNetUsers_EmployeeId",
                table: "DeclarationForms");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationForms_EmployeeId",
                table: "DeclarationForms");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "DeclarationForms",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "DeclarationForms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationForms_EmployeeId1",
                table: "DeclarationForms",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationForms_AspNetUsers_EmployeeId1",
                table: "DeclarationForms",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
