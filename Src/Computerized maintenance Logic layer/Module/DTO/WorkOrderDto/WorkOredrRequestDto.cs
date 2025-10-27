using computrized_maintenance_Data_Access.Enumes;

namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{
    public sealed record WorkOredrRequestDto
      (
            int ID,
            string WorkOrderNumber,
            string Description,
            int AssetID,
            workOrderStatus Status,
            int CreatedByID,
            int AssignedToID,
            DateTime CreatedDate,
            DateTime StartDate,
            DateTime DueDate,
            DateTime? CompeletedDate,
            string? Note

        );
}
