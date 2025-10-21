using computrized_maintenance_Data_Access.Entites.InventoryManagement;

namespace computrized_maintenance_Data_Access.Entites.WorkOrderManagement
{
    /// <summary>
    /// Tracking which spare parts were used during Task 
    /// </summary>
    public sealed class WorkOrderPart
    {

        public int ID { get; set; }
        public int WO_ID { get; set; }
        public int PartItemID {  get; set; }
        public short QuantityUsed { get; set; }


        public WorkOrder WorkOrder { get; set; }= null!;
        public InventoryItem InventoryItem { get; set; } = null!;
    }
}
