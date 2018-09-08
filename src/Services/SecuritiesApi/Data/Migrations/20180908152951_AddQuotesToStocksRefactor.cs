using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuritiesApi.Data.Migrations
{
    public partial class AddQuotesToStocksRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Change",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Last",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "PercentChange",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "RetrievalDateTime",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Shares",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "AverageVolume",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "AverageVolume30",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DayHigh",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DayLow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Dividend",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Open",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "YearHigh",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "YearLow",
                table: "Securities");

            migrationBuilder.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayHigh = table.Column<decimal>(nullable: false),
                    DayLow = table.Column<decimal>(nullable: false),
                    Open = table.Column<decimal>(nullable: false),
                    Close = table.Column<decimal>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    MovingAverage10 = table.Column<decimal>(nullable: false),
                    Macd8179 = table.Column<decimal>(nullable: false),
                    StochasticsSlowK1450 = table.Column<decimal>(nullable: false),
                    StockId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quote_Securities_StockId",
                        column: x => x.StockId,
                        principalTable: "Securities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quote_StockId",
                table: "Quote",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quote");

            migrationBuilder.AddColumn<decimal>(
                name: "Change",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Last",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "RetrievalDateTime",
                table: "Securities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Shares",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageVolume",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageVolume30",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DayHigh",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DayLow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Dividend",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketCap",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Open",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "YearHigh",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "YearLow",
                table: "Securities",
                nullable: true);
        }
    }
}
