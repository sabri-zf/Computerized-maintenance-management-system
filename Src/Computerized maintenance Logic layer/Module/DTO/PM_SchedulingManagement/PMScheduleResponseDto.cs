namespace Computerized_maintenance_Logic_layer.Module.DTO.PM_SchedulingManagement
{
    public sealed record PMScheduleResponseDto
   (
        int PmID,
        DateTime NextDueDate,
        DateTime? LastCompletionDate,
        bool IsActive,
        DateTime CreatedDate
    );

}
