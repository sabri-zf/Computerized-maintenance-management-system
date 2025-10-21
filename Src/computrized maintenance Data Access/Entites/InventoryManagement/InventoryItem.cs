using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;

namespace computrized_maintenance_Data_Access.Entites.InventoryManagement
{

    /// <summary>
    /// Main table that stores each spare part or item
    /// </summary>
    public sealed class InventoryItem
    {
        public int ID { get; set; }
        public string ItemName { get; set; } = null!;
        public string PartNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public short Quintity { get; set; }
        public short ReorderLevel { get; set; }
        public decimal UnitCost { get; set; }
        public int LocationID { get; set; }
        public bool IsActive { get; set; }



        public Location Location { get; set; } = null!;
        public ICollection<WorkOrderPart> WorkOrderParts { get; set; } = new List<WorkOrderPart>();
        public ICollection<InventoryTransaction> Transactions { get; set; } = new List<InventoryTransaction>();

    }
}
