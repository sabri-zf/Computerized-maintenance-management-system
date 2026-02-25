using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement
{
    public class PreventiveMaintenance
    {

        public int ID { get; set; }
        public int AssetID { get; set; }
        public string TaskDescription { get; set; } = null!;
        public En_FrequencyTask Frequency { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }


        public Asset Asset { get; set; } = null!;
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public ICollection<PMSchedule> PMSchedules { get; set; } = new List<PMSchedule>();
    }
}
