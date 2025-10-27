namespace Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto
{
    /// <summary>
    ///  Response object To recieve a Data from Server
    /// </summary>
    /// <param name="ItemName"> Part Name</param>
    /// <param name="PartNumber"> Part Number</param>
    /// <param name="Description"> More details about Part</param>
    /// <param name="Quintity">How many have parts on warehouse</param>
    /// <param name="ReorderLevel">the amount of quintity to reorder</param>
    /// <param name="UnitCost">Actual Cost of Part</param>
    /// <param name="LocationID">where the parts resied</param>
    /// <param name="IsActive">chcke out is exist or not </param>
    public sealed record InventoryItemResponseDto
        (
            string ItemName,
            string PartNumber ,
            string Description,
            short Quintity ,
            short ReorderLevel ,
            decimal UnitCost ,
            int LocationID ,
            bool IsActive 
        );


}
