using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StainlessMarketApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FasonProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    ProcessType = table.Column<string>(type: "TEXT", nullable: false),
                    Quality = table.Column<string>(type: "TEXT", nullable: false),
                    SurfaceFinish = table.Column<string>(type: "TEXT", nullable: false),
                    Thickness = table.Column<decimal>(type: "TEXT", nullable: false),
                    Width = table.Column<decimal>(type: "TEXT", nullable: false),
                    Length = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    EntryDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasonProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StokProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Quality = table.Column<string>(type: "TEXT", nullable: false),
                    SurfaceFinish = table.Column<string>(type: "TEXT", nullable: false),
                    Thickness = table.Column<decimal>(type: "TEXT", nullable: false),
                    Width = table.Column<decimal>(type: "TEXT", nullable: false),
                    Length = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    EntryDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokProducts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FasonProducts");

            migrationBuilder.DropTable(
                name: "StokProducts");
        }
    }
}
