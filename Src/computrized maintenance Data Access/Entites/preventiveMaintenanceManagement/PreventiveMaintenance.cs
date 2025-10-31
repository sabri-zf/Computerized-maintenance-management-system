using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.preventiveMaintenanceManagement
{
    public class PreventiveMaintenance
    {

        public int ID { get; set; }
        public int AssetID { get; set; }
        public string TaskDescription { get; set; } = null!;
        public EnFrequencyTask Frequency { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime NextDueDate { get; set; }


        public Asset Asset { get; set; } = null!;
        public ICollection<WorkOrder> workOrders { get; set; } = new List<WorkOrder>();
    }
}
