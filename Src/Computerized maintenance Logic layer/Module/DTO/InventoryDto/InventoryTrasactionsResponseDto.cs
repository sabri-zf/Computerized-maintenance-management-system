namespace Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto
{

    /// <summary>
    /// Data Transfare Object Of <see cref="InventoryTrasactionsResponseDto"/> to send data from server
    /// </summary>
    public sealed record InventoryTrasactionsResponseDto
    (
           int InventoryItemID,
           string Type,
           short Quntity,
           DateTime TransactionDate,
           string? Reference
    );

}
