using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuritiesApi.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Change = table.Column<decimal>(nullable: false),
                    PercentChange = table.Column<decimal>(nullable: false),
                    Last = table.Column<decimal>(nullable: false),
                    Shares = table.Column<decimal>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    RetrievalDateTime = table.Column<DateTime>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    MorningStarRating = table.Column<int>(nullable: true),
                    DayHigh = table.Column<decimal>(nullable: true),
                    DayLow = table.Column<decimal>(nullable: true),
                    Dividend = table.Column<decimal>(nullable: true),
                    Open = table.Column<decimal>(nullable: true),
                    Volume = table.Column<decimal>(nullable: true),
                    YearHigh = table.Column<decimal>(nullable: true),
                    YearLow = table.Column<decimal>(nullable: true),
                    AverageVolume = table.Column<decimal>(nullable: true),
                    AverageVolume30 = table.Column<decimal>(nullable: true),
                    MarketCap = table.Column<decimal>(nullable: true),
                    ExchangeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Securities_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Securities_ExchangeId",
                table: "Securities",
                column: "ExchangeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Securities");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
