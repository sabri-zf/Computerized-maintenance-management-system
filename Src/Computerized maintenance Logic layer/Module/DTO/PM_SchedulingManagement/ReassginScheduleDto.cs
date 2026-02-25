using computrized_maintenance_Data_Access.Enumes;

namespace Computerized_maintenance_Logic_layer.Module.DTO.PM_SchedulingManagement
{
    public sealed record ReassginScheduleDto
    (
        int SecheduleId,
        string FrequencyTask
    );
}
