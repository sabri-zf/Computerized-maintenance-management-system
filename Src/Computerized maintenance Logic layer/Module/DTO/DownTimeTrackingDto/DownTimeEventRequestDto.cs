namespace Computerized_maintenance_Logic_layer.Module.DTO.DownTimeTrackingDto
{
    /// <summary>
    /// Record to dealing with incoming Request data come from Client or 
    /// to repersent Data into dataset 
    /// </summary>
    public sealed record DownTimeEventRequestDto
 (
      int ID,
      int AssetID,
      int? WO_ID,
      DateTime StartDownTimeEvent,
      DateTime? EndDownTimeEvent,
      string DownTimeType,
      string Reason,
      string? ActionTaken,
      int PerformedByID
 );
}
