using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sub_Category_Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubCategories_AssetCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "AssetCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssetTagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufactuerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManufactuerModelNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    PurchaseCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WarrantyExpiryDate = table.Column<DateTime>(type: "date", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AssetCategoryID = table.Column<int>(type: "int", nullable: false),
                    AssetLocationID = table.Column<int>(type: "int", nullable: false),
                    AssetStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeterReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Criticality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAssetDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateAssetDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assets_AssetCategories_AssetCategoryID",
                        column: x => x.AssetCategoryID,
                        principalTable: "AssetCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Locations_AssetLocationID",
                        column: x => x.AssetLocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageWidth = table.Column<short>(type: "smallint", nullable: false),
                    ImageHeight = table.Column<short>(type: "smallint", nullable: false),
                    AssetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssetImages_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssetCategories",
                columns: new[] { "ID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Production Equipment" },
                    { 2, "Facility Infrastructure" },
                    { 3, "Transportation" },
                    { 4, "IT Infrastructure" },
                    { 5, "Safety Equipment" },
                    { 6, "Tools" },
                    { 7, "Mechanical" },
                    { 8, "Electrical" },
                    { 9, "Electronic" },
                    { 10, "Hydraulic" },
                    { 11, "Pneumatic" },
                    { 12, "Process Equipment" },
                    { 13, "Support Equipment" },
                    { 14, "Standby Equipment" },
                    { 15, "Fire Protection" },
                    { 16, "Office Equipment" },
                    { 17, "Medical Equipment" },
                    { 18, "Laboratory Equipment" },
                    { 19, "Construction Equipment" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "LocationName" },
                values: new object[,]
                {
                    { 1, "Building A" },
                    { 2, "Building B" },
                    { 3, "facility - 109" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "ID", "AssetCategoryID", "AssetLocationID", "AssetName", "AssetStatus", "AssetTagNumber", "CreateAssetDate", "CreateByUser", "Criticality", "InstallationDate", "ManufactuerModelNumber", "ManufactuerName", "MeterReading", "PurchaseCost", "PurchaseDate", "UpdateAssetDate", "WarrantyExpiryDate" },
                values: new object[,]
                {
                    { 1, 1, 2, "HVAC Unit - Floor 3", "Active", "HVAC-3F-01", new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "High", new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "24VNA9", "Carrier", "Hours", 12500.00m, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 1, "Forklift #5", "UnderMaintenance", "FL-005", new DateTime(2021, 8, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, "Medium", null, "8FGCU25", "Toyota", "Hours", 32000.00m, new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 15, 20, 12, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, "Server Rack UPS", "Active", "IT-UPS-02", new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "High", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "SMX1500RM2U", "APC", "PowerCycles", 2200.00m, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "ID", "CategoryID", "Sub_Category_Name" },
                values: new object[,]
                {
                    { 1, 1, "CNC Machines" },
                    { 2, 1, "Assembly Lines" },
                    { 3, 2, "HVAC Systems" },
                    { 4, 2, "Electrical Systems" }
                });

            migrationBuilder.InsertData(
                table: "AssetImages",
                columns: new[] { "ID", "AssetID", "ImageHeight", "ImagePath", "ImageWidth" },
                values: new object[,]
                {
                    { 1, 1, (short)150, "C:\\User\\Samsung\\Images\\imageOne1.png", (short)150 },
                    { 2, 1, (short)150, "C:\\User\\Samsung\\Images\\imageOne2.png", (short)150 },
                    { 3, 2, (short)150, "C:\\User\\Samsung\\Images\\imageTwo1.png", (short)150 },
                    { 4, 2, (short)150, "C:\\User\\Samsung\\Images\\imageTwo2.png", (short)150 },
                    { 5, 3, (short)150, "C:\\User\\Samsung\\Images\\imageThree1.png", (short)150 },
                    { 6, 3, (short)150, "C:\\User\\Samsung\\Images\\imageThree2.png", (short)150 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetImages_AssetID",
                table: "AssetImages",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetCategoryID",
                table: "Assets",
                column: "AssetCategoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetName",
                table: "Assets",
                column: "AssetName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetLocationID",
                table: "Assets",
                column: "AssetLocationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryID",
                table: "SubCategories",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetImages");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetCategories");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
