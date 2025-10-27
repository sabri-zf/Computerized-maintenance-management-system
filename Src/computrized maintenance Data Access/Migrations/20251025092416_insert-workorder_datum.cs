using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace computrized_maintenance_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class insertworkorder_datum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "maintain",
                table: "WorkOrders",
                columns: new[] { "ID", "AssetID", "AssignedToID", "CompeletedDate", "CreatedByID", "CreatedDate", "Description", "DueDate", "Note", "StartDate", "Status", "WorkOrderNumber" },
                values: new object[,]
                {
                    { 1, 3, 1, null, 9, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Replace air filter in HVAC unit.", new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check stock for spare filters before starting.", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open", "WO-0001" },
                    { 2, 2, 1, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) ,5, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inspect safety valves on boiler system.", new DateTime(2025, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "All valves passed inspection.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", "WO-0002" }
                });

            //migrationBuilder.InsertData(
            //    schema: "maintain",
            //    table: "WorkOrderHisties",
            //    columns: new[] { "ID", "Action", "ActionDate", "PerformedActionByID", "WO_ID" },
            //    values: new object[,]
            //    {
            //        { 1, "Created", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
            //        { 2, "Assgined", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
            //        { 3, "Created", new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2 },
            //        { 4, "Completed", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 }
            //    });

            migrationBuilder.InsertData(
                schema: "maintain",
                table: "WorkOrderParts",
                columns: new[] { "ID", "PartItemID", "QuantityUsed", "WO_ID" },
                values: new object[,]
                {
                    { 1, 2, (short)2, 1 },
                    { 2, 5, (short)1, 1 },
                    { 3, 1, (short)3, 2 }
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
