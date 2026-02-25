using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class made_new_entity_Schedule_pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextDueDate",
                schema: "maintain",
                table: "PreventiveMaintenances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompeletedDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "maintain",
                table: "WorkOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "PreventiveMaintenances",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "maintain",
                table: "PreventiveMaintenances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "maintain",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PM_Id = table.Column<int>(type: "int", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastCompletionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleID)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Schedules_PreventiveMaintenance",
                        column: x => x.PM_Id,
                        principalSchema: "maintain",
                        principalTable: "PreventiveMaintenances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "PreventiveMaintenances",
                keyColumn: "ID",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "PreventiveMaintenances",
                keyColumn: "ID",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.InsertData(
                schema: "maintain",
                table: "Schedules",
                columns: new[] { "ScheduleID", "IsActive", "LastCompletionDate", "NextDueDate", "PM_Id" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 2, 22, 22, 30, 10, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 11, 20, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, true, null, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, true, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, false, null, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 1,
                column: "Type",
                value: "corrective");

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 2,
                column: "Type",
                value: "corrective");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_NextDueDate",
                schema: "maintain",
                table: "Schedules",
                column: "NextDueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PM_Id",
                schema: "maintain",
                table: "Schedules",
                column: "PM_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "maintain");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "maintain",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "maintain",
                table: "PreventiveMaintenances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompeletedDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "PreventiveMaintenances",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextDueDate",
                schema: "maintain",
                table: "PreventiveMaintenances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "PreventiveMaintenances",
                keyColumn: "ID",
                keyValue: 1,
                column: "NextDueDate",
                value: new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "PreventiveMaintenances",
                keyColumn: "ID",
                keyValue: 2,
                column: "NextDueDate",
                value: new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
