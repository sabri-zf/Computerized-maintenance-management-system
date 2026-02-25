namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{
    public sealed record WorkOrderHistoryResponseDto
   (
        int WorkOrderID,
        string Action,
        DateTime ActionDate,
        int PerformedActionByID
        );
}
