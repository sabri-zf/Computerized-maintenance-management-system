namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{

    public sealed record WorkOredrList_view
    (
            string WorkOrderNumber,
            string Description,
            string AssetName,
            string Type,
            string Priority,
            string Status,
            string TechnicianName,
            DateTime DueDate,
            DateTime CreatedDate,
            string? Note
    );
}
