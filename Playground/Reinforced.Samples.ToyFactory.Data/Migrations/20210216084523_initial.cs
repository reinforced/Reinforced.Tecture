using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reinforced.Samples.ToyFactory.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemsCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceSupplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    StockQuantity = table.Column<double>(type: "REAL", nullable: false),
                    MeasurementUnitId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_MeasurementUnits_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplyStatusHistoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Previous = table.Column<int>(type: "INTEGER", nullable: true),
                    Next = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResourceSupplyId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Blueprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToyTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blueprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blueprints_ToyTypes_ToyTypeId",
                        column: x => x.ToyTypeId,
                        principalTable: "ToyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplyItems",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceSupplyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false)
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
                name: "BlueprintResources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "INTEGER", nullable: false),
                    BlueprintId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlueprintResources", x => new { x.BlueprintId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_BlueprintResources_Blueprints_BlueprintId",
                        column: x => x.BlueprintId,
                        principalTable: "Blueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlueprintResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlueprintResources_ResourceId",
                table: "BlueprintResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Blueprints_ToyTypeId",
                table: "Blueprints",
                column: "ToyTypeId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlueprintResources");

            migrationBuilder.DropTable(
                name: "ResourceSupplyItems");

            migrationBuilder.DropTable(
                name: "ResourceSupplyStatusHistoryItems");

            migrationBuilder.DropTable(
                name: "Blueprints");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "ResourceSupplies");

            migrationBuilder.DropTable(
                name: "ToyTypes");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");
        }
    }
}
