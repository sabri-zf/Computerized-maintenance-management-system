using computrized_maintenance_Data_Access.Entites.InventoryManagement;
using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.WorkOrderManagement
{
    /// <summary>
    /// Keeps history of stock movements (IN/OUT)
    /// </summary>
    public sealed class InventoryTransaction
    {
        public int ID { get; set; }
        public int InventoryItemID { get; set; }
        public En_TransactionType Type { get; set; }
        public short Quntity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Reference { get; set; }


        public InventoryItem InventoryItem { get; set; } = null!;
    }
}
