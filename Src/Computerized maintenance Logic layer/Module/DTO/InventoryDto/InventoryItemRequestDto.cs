namespace Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto
{
    /// <summary>
    ///  Request object To Send a Data To Server
    /// </summary>
    /// <param name="ID">unique identyfire</param>
    /// <param name="ItemName"> Part Name</param>
    /// <param name="PartNumber"> Part Number</param>
    /// <param name="Description"> More details about Part</param>
    /// <param name="Quintity">How many have parts on warehouse</param>
    /// <param name="ReorderLevel">the amount of quintity to reorder</param>
    /// <param name="UnitCost">Actual Cost of Part</param>
    /// <param name="LocationID">where the parts resied</param>
    /// <param name="IsActive">chcke out is exist or not </param>
    public record InventoryItemRequestDto
       (
           int ID,
           string ItemName,
           string PartNumber,
           string Description,
           short Quintity,
           short ReorderLevel,
           decimal UnitCost,
           int LocationID,
           bool IsActive
       );


}
