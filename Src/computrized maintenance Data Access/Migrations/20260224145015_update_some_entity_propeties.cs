using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class update_some_entity_propeties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "Schedules",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()"
                );

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "maintain",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "maintain",
                table: "PreventiveMaintenances",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 29, 12, 30, 0, 0, DateTimeKind.Unspecified));
            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 29, 12, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 29, 12, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 2, 12, 30, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "maintain",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "maintain",
                table: "Schedules");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "maintain",
                table: "PreventiveMaintenances",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
