using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class add_report_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    StartPeriod = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndPeriod = table.Column<DateTime>(type: "datetime", nullable: false),
                    Interval_Running_machine = table.Column<float>(type: "real", nullable: false),
                    Mean_Time_between_Failure = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Mean_Time_TO_Repair = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Availability = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Mean_DownTime = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ID)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Reports_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ID", "AssetID", "Availability", "CreateAt", "EndPeriod", "Interval_Running_machine", "Mean_DownTime", "Mean_Time_between_Failure", "Mean_Time_TO_Repair", "StartPeriod" },
                values: new object[,]
                {
                    { 1, 1, 0.92m, new DateTime(2021, 8, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.5f, 2.5m, 12.4m, 1.7m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 0.88m, new DateTime(2021, 8, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 250f, 3.4m, 15.3m, 2.1m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AssetID",
                table: "Reports",
                column: "AssetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
