using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class add_PreventiveMaintenanceEntitiy_withSeedDataOfIt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreventiveMaintenanceID",
                schema: "maintain",
                table: "WorkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PreventiveMaintenances",
                schema: "maintain",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreventiveMaintenances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreventiveMaintenances_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "maintain",
                table: "PreventiveMaintenances",
                columns: new[] { "ID", "AssetID", "CreatedDate", "Frequency", "NextDueDate", "TaskDescription" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quarterly", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Replace air filter in HVAC unit every 3 months." },
                    { 2, 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SemiAnnual", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inspect safety valves on boiler system every 6 months." }
                });

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 1,
                column: "PreventiveMaintenanceID",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 2,
                column: "PreventiveMaintenanceID",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PreventiveMaintenanceID",
                schema: "maintain",
                table: "WorkOrders",
                column: "PreventiveMaintenanceID");

            migrationBuilder.CreateIndex(
                name: "IX_PreventiveMaintenances_AssetID",
                schema: "maintain",
                table: "PreventiveMaintenances",
                column: "AssetID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_PreventiveMaintenances_PreventiveMaintenanceID",
                schema: "maintain",
                table: "WorkOrders",
                column: "PreventiveMaintenanceID",
                principalSchema: "maintain",
                principalTable: "PreventiveMaintenances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_PreventiveMaintenances_PreventiveMaintenanceID",
                schema: "maintain",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "PreventiveMaintenances",
                schema: "maintain");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_PreventiveMaintenanceID",
                schema: "maintain",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "PreventiveMaintenanceID",
                schema: "maintain",
                table: "WorkOrders");
        }
    }
}
