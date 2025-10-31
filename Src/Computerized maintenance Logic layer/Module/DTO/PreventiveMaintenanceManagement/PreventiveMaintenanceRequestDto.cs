namespace Computerized_maintenance_Logic_layer.Module.DTO.PreventiveMaintenanceManagement
{
    /// <summary>
    /// Data transfer object for Preventive Maintenance Response
    /// </summary>

    public sealed record PreventiveMaintenanceRequesteDto
    (
        int ID,
        int AssetID,
        string TaskDescription,
        string Frequency,
        DateTime ScheduledDate,
        DateTime NextScheduleDate
    );
}
