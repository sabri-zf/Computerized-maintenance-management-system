namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{
    public sealed record WorkOrderPartRequestDto
  (
        int ID,
       int WorkOrderID,
       int PartItemID,
       short QuantityUsed
       );
}
