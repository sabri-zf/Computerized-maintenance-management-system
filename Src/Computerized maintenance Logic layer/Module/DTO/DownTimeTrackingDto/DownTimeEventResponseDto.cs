namespace Computerized_maintenance_Logic_layer.Module.DTO.DownTimeTrackingDto
{
    /// <summary>
    /// Record to dealing with Outgoing response data come from data-set or 
    /// to repersent Data for client 
    /// </summary>
    public sealed record DownTimeEventResponseDto
   (
        int AssetID,
        int? WO_ID,
        DateTime StartDownTimeEvent,
        DateTime? EndDownTimeEvent,
        Single? TimeDuration,
        string DownTimeType,
        string Reason,
        string? ActionTaken,
        int PerformedByID,
        DateTime? CreateAt
   );
}
