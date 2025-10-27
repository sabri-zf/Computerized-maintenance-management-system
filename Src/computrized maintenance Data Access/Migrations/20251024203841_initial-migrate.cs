using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Inventory");

            migrationBuilder.EnsureSchema(
                name: "maintain");

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
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssetCategoryID = table.Column<int>(type: "int", nullable: false),
                    AssetLocationID = table.Column<int>(type: "int", nullable: false),
                    AssetStatus = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    MeterReading = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Criticality = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CreateAssetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAssetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "InventoryItems",
                schema: "Inventory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Quintity = table.Column<short>(type: "smallint", nullable: false),
                    ReorderLevel = table.Column<short>(type: "smallint", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Locations_LocationID",
                        column: x => x.LocationID,
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

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                schema: "maintain",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    CreatedByID = table.Column<int>(type: "int", nullable: false),
                    AssignedToID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Technicians_AssignedToID",
                        column: x => x.AssignedToID,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransactions",
                schema: "Inventory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryItemID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Quntity = table.Column<short>(type: "smallint", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_InventoryItems_InventoryItemID",
                        column: x => x.InventoryItemID,
                        principalSchema: "Inventory",
                        principalTable: "InventoryItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderHisties",
                schema: "maintain",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WO_ID = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformedActionByID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderHisties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkOrderHisties_WorkOrders_WO_ID",
                        column: x => x.WO_ID,
                        principalSchema: "maintain",
                        principalTable: "WorkOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                       name: "FK_WorkOrders_Users_PerformedActionByID",
                       column: x => x.PerformedActionByID,
                       principalTable: "Users",
                       principalColumn: "UserID",
                       onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderParts",
                schema: "maintain",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WO_ID = table.Column<int>(type: "int", nullable: false),
                    PartItemID = table.Column<int>(type: "int", nullable: false),
                    QuantityUsed = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderParts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkOrderParts_InventoryItems_PartItemID",
                        column: x => x.PartItemID,
                        principalSchema: "Inventory",
                        principalTable: "InventoryItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderParts_WorkOrders_WO_ID",
                        column: x => x.WO_ID,
                        principalSchema: "maintain",
                        principalTable: "WorkOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    { 1, "Main Warehouse" },
                    { 2, "Secondary Storage" },
                    { 3, "Outdoor Yard" }
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
                schema: "Inventory",
                table: "InventoryItems",
                columns: new[] { "ID", "Description", "IsActive", "ItemName", "LocationID", "PartNumber", "Quintity", "ReorderLevel", "UnitCost" },
                values: new object[,]
                {
                    { 1, "Standard HVAC air filter, 20x25x2 inches.", true, "Air Filter", 1, "AF-1001", (short)47, (short)10, 12.50m },
                    { 2, "High-pressure hydraulic hose, 2-meter length.", true, "Hydraulic Hose", 2, "HH-2045", (short)18, (short)5, 45.75m },
                    { 3, "Synthetic lubricant oil, 5-liter container.", true, "Lubricant Oil", 1, "LO-3020", (short)15, (short)5, 38.90m },
                    { 4, "Industrial safety valve, 2-inch diameter.", false, "Safety Valve", 3, "SV-1550", (short)0, (short)3, 120.00m },
                    { 5, "Replacement motor for conveyor belt system.", true, "Conveyor Belt Motor", 2, "CBM-450", (short)1, (short)1, 350.00m },
                    { 6, "Digital pressure sensor, 0–10 bar range.", false, "Pressure Sensor", 3, "PS-8812", (short)0, (short)4, 78.40m }
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

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "InventoryTransactions",
                columns: new[] { "ID", "InventoryItemID", "Quntity", "Reference", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 1, 1, (short)100, null, new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "In" },
                    { 2, 1, (short)20, "WO-0001", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out" },
                    { 3, 2, (short)10, null, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "In" },
                    { 4, 2, (short)3, null, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out" },
                    { 5, 3, (short)2, null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out" }
                });

           

         

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetLocationID",
                table: "Assets",
                column: "AssetLocationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_LocationID",
                schema: "Inventory",
                table: "InventoryItems",
                column: "LocationID");
           
            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderHisties_WO_ID",
                schema: "maintain",
                table: "WorkOrderHisties",
                column: "WO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderParts_PartItemID",
                schema: "maintain",
                table: "WorkOrderParts",
                column: "PartItemID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderParts_WO_ID",
                schema: "maintain",
                table: "WorkOrderParts",
                column: "WO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssetID",
                schema: "maintain",
                table: "WorkOrders",
                column: "AssetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetImages");

            migrationBuilder.DropTable(
                name: "InventoryTransactions",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "WorkOrderHisties",
                schema: "maintain");

            migrationBuilder.DropTable(
                name: "WorkOrderParts",
                schema: "maintain");

            migrationBuilder.DropTable(
                name: "InventoryItems",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "WorkOrders",
                schema: "maintain");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetCategories");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
