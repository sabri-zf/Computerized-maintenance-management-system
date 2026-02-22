using computrized_maintenance_Data_Access.Enumes;

namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{
    public sealed record WorkOrderHistoryRequestDto
   (
        int ID,
        int WorkOrderID,
        En_WorkOrderHistoryActionStatus Action,
        DateTime ActionDate,
        int PerformedActionByID
        );
}
