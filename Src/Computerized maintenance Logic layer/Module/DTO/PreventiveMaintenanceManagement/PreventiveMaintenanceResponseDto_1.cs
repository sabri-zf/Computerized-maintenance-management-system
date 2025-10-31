namespace Computerized_maintenance_Logic_layer.Module.DTO.PreventiveMaintenanceManagement
{
    /// <summary>
    /// Data transfer object for Preventive Maintenance Request
    /// </summary>
    public sealed record PreventiveMaintenanceResponseDto
    (
        int AssetID,
        string TaskDescription,
        string Frequency,
        DateTime ScheduledDate,
        DateTime NextScheduleDate
    );
}
