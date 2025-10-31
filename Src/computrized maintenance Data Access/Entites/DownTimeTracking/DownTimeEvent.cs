using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.DownTimeTracking
{
    public sealed class DownTimeEvent
    {
       public int ID { get; set; }
       public int AssetID { get; set; }
       public int? WO_ID { get; set; }
       public DateTime StartDownTimeEvent { get; set; }
       public DateTime? EndDownTimeEvent { get; set; }
       public EnDownTimeType DownTimeType { get; set; }
       public string Reason { get; set; } = null!;
       public string? ActionTaken { get; set; }
       public int PerformedByID { get; set; }
       public DateTime CreateAt { get; set; }

       
        public Asset Asset { get; set; } = null!;
        public WorkOrder? WorkOrder { get; set; }
    }
}
