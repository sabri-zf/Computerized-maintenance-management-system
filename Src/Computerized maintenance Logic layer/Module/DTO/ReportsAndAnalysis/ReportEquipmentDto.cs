using CMMS_Api.DTO;

namespace Computerized_maintenance_Logic_layer.Module.DTO.ReportsAndAnalysis
{
    /// <summary>
    /// record represent data transfer object of Report equipment
    /// </summary>
   
    public sealed record ReportEquipmentDto
    (
        int AsseID,DateTime StartPeriod,DateTime EndPeriod,float MTTR,float MTBF,float MDT,
        float Availability,float Interval_Running_Machine,DateTime CreateAt
    );

   
}
