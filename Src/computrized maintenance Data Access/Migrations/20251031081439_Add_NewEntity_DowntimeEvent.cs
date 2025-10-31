using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class Add_NewEntity_DowntimeEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "report");

            migrationBuilder.CreateTable(
                name: "DownTimeEvents",
                schema: "report",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    WO_ID = table.Column<int>(type: "int", nullable: true),
                    StartDownTimeEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDownTimeEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DownTimeType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformedByID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownTimeEvents", x => x.ID)
                        .Annotation("SqlServer:Clustered", true);

                    table.ForeignKey(
                        name: "FK_DownTimeEvents_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_DownTimeEvents_WorkOrders_WO_ID",
                        column: x => x.WO_ID,
                        principalSchema: "maintain",
                        principalTable: "WorkOrders",
                        principalColumn: "ID");

                    table.ForeignKey(
                       name: "FK_DownTimeEvents_Users_PerformedByID",
                       column: x => x.PerformedByID,
                       principalSchema: "dbo",
                       principalTable: "Users",
                       principalColumn: "UserID");
                });

            migrationBuilder.InsertData(
                schema: "report",
                table: "DownTimeEvents",
                columns: new[] { "ID", "ActionTaken", "AssetID", "CreateAt", "DownTimeType", "EndDownTimeEvent", "PerformedByID", "Reason", "StartDownTimeEvent", "WO_ID" },
                values: new object[,]
                {
                    { 1, "Replaced bearing and added lubrication", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unplanned", new DateTime(2025, 10, 10, 10, 45, 0, 0, DateTimeKind.Unspecified), 6, "Motor overheating due to lack of lubrication", new DateTime(2025, 10, 10, 8, 15, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Installed new firmware and tested functionality", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Planned", new DateTime(2025, 10, 12, 17, 30, 0, 0, DateTimeKind.Unspecified), 6, "Scheduled control system upgrade", new DateTime(2025, 10, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Recalibrated sensor and updated firmware", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unplanned", new DateTime(2025, 10, 15, 9, 45, 0, 0, DateTimeKind.Unspecified), 6, "Unexpected sensor calibration issue", new DateTime(2025, 10, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownTimeEvents_AssetID",
                schema: "report",
                table: "DownTimeEvents",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_DownTimeEvents_WO_ID",
                schema: "report",
                table: "DownTimeEvents",
                column: "WO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownTimeEvents",
                schema: "report");
        }
    }
}
