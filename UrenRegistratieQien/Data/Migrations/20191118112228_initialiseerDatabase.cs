using Microsoft.EntityFrameworkCore.Migrations;

namespace UrenRegistratieQien.Data.Migrations
{
    public partial class initialiseerDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    Contact1Name = table.Column<string>(nullable: true),
                    Contact2Name = table.Column<string>(nullable: true),
                    Contact1Phone = table.Column<int>(nullable: false),
                    Contact2Phone = table.Column<int>(nullable: false),
                    Contact1Email = table.Column<string>(nullable: true),
                    Contact2Email = table.Column<string>(nullable: true),
                    CompanyPhone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeclarationForms",
                columns: table => new
                {
                    DeclarationFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    Month = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    Submitted = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarationForms", x => x.DeclarationFormId);
                    table.ForeignKey(
                        name: "FK_DeclarationForms_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HourRows",
                columns: table => new
                {
                    HourRowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(nullable: true),
                    Worked = table.Column<int>(nullable: false),
                    Overtime = table.Column<int>(nullable: false),
                    Sickness = table.Column<int>(nullable: false),
                    Vacation = table.Column<int>(nullable: false),
                    Holiday = table.Column<int>(nullable: false),
                    Training = table.Column<int>(nullable: false),
                    Other = table.Column<int>(nullable: false),
                    OtherExplanation = table.Column<string>(nullable: true),
                    DeclarationFormId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourRows", x => x.HourRowId);
                    table.ForeignKey(
                        name: "FK_HourRows_DeclarationForms_DeclarationFormId",
                        column: x => x.DeclarationFormId,
                        principalTable: "DeclarationForms",
                        principalColumn: "DeclarationFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationForms_EmployeeId",
                table: "DeclarationForms",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClientId",
                table: "Employees",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_HourRows_DeclarationFormId",
                table: "HourRows",
                column: "DeclarationFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourRows");

            migrationBuilder.DropTable(
                name: "DeclarationForms");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
