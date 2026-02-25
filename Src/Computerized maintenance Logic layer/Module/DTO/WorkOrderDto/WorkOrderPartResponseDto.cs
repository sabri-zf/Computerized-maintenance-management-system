namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{
    public sealed record WorkOrderPartResponseDto
   (
        int WorkOrderID,
        int PartItemID,
        short QuantityUsed
        );
}
