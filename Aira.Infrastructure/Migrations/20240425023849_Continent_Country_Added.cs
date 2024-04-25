using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Continent_Country_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContinentCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    ContinentName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsoLanguageCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GmtOffset = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Iso3 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsoLanguageCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    ContinentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_ContinentId",
                table: "Country",
                column: "ContinentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Continent");
        }
    }
}
