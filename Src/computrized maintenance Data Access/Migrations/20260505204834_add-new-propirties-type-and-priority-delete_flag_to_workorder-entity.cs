using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class addnewpropirtiestypeandprioritydelete_flag_to_workorderentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "maintain",
                table: "WorkOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                schema: "maintain",
                table: "WorkOrders",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "IsDeleted", "Priority", "Type" },
                values: new object[] { false, "medium", "Preventive" });

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "IsDeleted", "Priority" },
                values: new object[] { false, "low" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "maintain",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "maintain",
                table: "WorkOrders");

            migrationBuilder.UpdateData(
                schema: "maintain",
                table: "WorkOrders",
                keyColumn: "ID",
                keyValue: 1,
                column: "Type",
                value: "corrective");
        }
    }
}
