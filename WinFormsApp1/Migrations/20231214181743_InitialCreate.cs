using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsApp1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowCount = table.Column<int>(type: "int", nullable: false),
                    ColumnCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileModelId = table.Column<int>(type: "int", nullable: true),
                    BalanceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceOpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceOpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceTurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceTurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountClasses_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FileModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balances_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Headers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Headers_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    OpeningBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalanceActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalancePassive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountClasses_AccountClassId",
                        column: x => x.AccountClassId,
                        principalTable: "AccountClasses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountClasses_FileModelId",
                table: "AccountClasses",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountClassId",
                table: "Accounts",
                column: "AccountClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Balances_FileModelId",
                table: "Balances",
                column: "FileModelId",
                unique: true,
                filter: "[FileModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Headers_FileModelId",
                table: "Headers",
                column: "FileModelId",
                unique: true,
                filter: "[FileModelId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Headers");

            migrationBuilder.DropTable(
                name: "AccountClasses");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
