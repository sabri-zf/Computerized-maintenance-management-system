namespace Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto
{
    /// <summary>
    /// Data Transfare Object Of <see cref="InventoryTrasactionsRequestDto"/> to receive data from server
    /// </summary>
    public sealed record InventoryTrasactionsRequestDto
    (
           int ID,
           int InventoryItemID,
           string Type,
           short Quntity,
           DateTime TransactionDate,
           string? Reference
    );

}
