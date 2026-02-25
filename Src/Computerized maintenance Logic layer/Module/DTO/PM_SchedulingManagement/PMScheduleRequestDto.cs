namespace Computerized_maintenance_Logic_layer.Module.DTO.PM_SchedulingManagement
{
    public sealed record PMScheduleRequestDto
  (
       int ScheduleID,
       int PmID,
       DateTime NextDueDate,
       DateTime? LastCompletionDate,
       bool IsActive,
       DateTime CreateDate
   );

}
