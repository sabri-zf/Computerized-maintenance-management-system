using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class fix_tribleofdynamicDataOnHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 10, 24, 21, 38, 40, 657, DateTimeKind.Local).AddTicks(6607));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "maintain",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 24, 21, 38, 40, 657, DateTimeKind.Local).AddTicks(6607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
