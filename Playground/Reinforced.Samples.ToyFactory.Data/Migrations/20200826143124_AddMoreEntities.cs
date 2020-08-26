using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reinforced.Samples.ToyFactory.Data.Migrations
{
    public partial class AddMoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeasurementUnitId",
                table: "Resources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "StockQuantity",
                table: "Resources",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ItemsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceSupplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplyItems",
                columns: table => new
                {
                    ResourceId = table.Column<int>(nullable: false),
                    ResourceSupplyId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceSupplyItems", x => new { x.ResourceSupplyId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_ResourceSupplyItems_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceSupplyItems_ResourceSupplies_ResourceSupplyId",
                        column: x => x.ResourceSupplyId,
                        principalTable: "ResourceSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplyStatusHistoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Previous = table.Column<int>(nullable: true),
                    Next = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ResourceSupplyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceSupplyStatusHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceSupplyStatusHistoryItems_ResourceSupplies_ResourceSupplyId",
                        column: x => x.ResourceSupplyId,
                        principalTable: "ResourceSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_MeasurementUnitId",
                table: "Resources",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplyItems_ResourceId",
                table: "ResourceSupplyItems",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplyStatusHistoryItems_ResourceSupplyId",
                table: "ResourceSupplyStatusHistoryItems",
                column: "ResourceSupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_MeasurementUnits_MeasurementUnitId",
                table: "Resources",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_MeasurementUnits_MeasurementUnitId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");

            migrationBuilder.DropTable(
                name: "ResourceSupplyItems");

            migrationBuilder.DropTable(
                name: "ResourceSupplyStatusHistoryItems");

            migrationBuilder.DropTable(
                name: "ResourceSupplies");

            migrationBuilder.DropIndex(
                name: "IX_Resources_MeasurementUnitId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "MeasurementUnitId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Resources");
        }
    }
}
