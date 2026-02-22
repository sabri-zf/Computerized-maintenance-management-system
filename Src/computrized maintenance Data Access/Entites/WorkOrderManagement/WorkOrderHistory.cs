using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.WorkOrderManagement
{
    /// <summary>
    /// Tracking the history of Each work order on the system
    /// </summary>
    public sealed class WorkOrderHistory
    {
        public int ID { get; set; }
        public int WO_ID { get; set; }
        public En_WorkOrderHistoryActionStatus Action { get; set; }
        public DateTime ActionDate { get; set; }
        public int PerformedActionByID {  get; set; }


        public WorkOrder WorkOrder { get; set; } = null!;
    }
}
