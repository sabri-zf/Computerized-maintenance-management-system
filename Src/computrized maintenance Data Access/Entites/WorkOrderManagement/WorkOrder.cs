using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.WorkOrderManagement
{

    /// <summary>
    /// Register workorder to do maintain operation task
    /// </summary>
    public sealed class WorkOrder
    {
        public int ID { get; set; }
        public string WorkOrderNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int AssetID { get; set; }
        public int CreatedByID { get; set; }  
        public int AssignedToID { get; set; }
        public workOrderStatus Status { get; set; } = workOrderStatus.Open;
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }

        /// <summary>
        /// target completion date for a work order
        /// </summary>
        public DateTime DueDate { get; set; }
        public DateTime? CompeletedDate { get; set; }
        public string? Note {  get; set; }



        // work order has many Asset , and Asset rely one workOrder

        public Asset Asset { get; set; } = null!;
        public ICollection<WorkOrderPart> UsedParts { get; set; } = new List<WorkOrderPart>();
        public ICollection<WorkOrderHistory> Histories { get; set; } = new List<WorkOrderHistory>();

    }
}
