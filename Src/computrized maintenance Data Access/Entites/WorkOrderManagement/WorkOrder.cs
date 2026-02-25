using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using computrized_maintenance_Data_Access.Enumes;

namespace computrized_maintenance_Data_Access.Entites.WorkOrderManagement
{

    /// <summary>
    /// Register workorder to do maintain operation task
    /// </summary>
    /// 

    // note: work order can be generated from preventive maintenance or can be generated manually by user
    // note: generating work order from preventive maintenance will be based on the schedule of preventive maintenance and the asset that is related to it, and the work order will be generated automatically by the system, but if the user want to generate work order manually
    // he can do that by filling out the required data for the work order and then the system will generate the work order number for him

    // toDo: work oreder would be has a type of maintenance type (preventive or corrective or modification)
    // PM => systmatic or conditional or predictive
    public sealed class WorkOrder
    {
        public int ID { get; set; }
        public string WorkOrderNumber { get; set; } = null!;// modify it to be auto generate number with prefix "WO" and 6 digit number
        public string Description { get; set; } = null!;
        public int PreventiveMaintenanceID { get; set; }
        public int AssetID { get; set; }
        public int CreatedByID { get; set; }
        public int AssignedToID { get; set; }
        public En_workOrderStatus Status { get; set; }
        public En_MaintenaceType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }

        /// <summary>
        /// target completion date for a work order
        /// </summary>
        public DateTime DueDate { get; set; }
        public DateTime? CompeletedDate { get; set; }
        public string? Note { get; set; }



        // work order has many Asset , and Asset rely one workOrder

        public Asset Asset { get; set; } = null!;
        public PreventiveMaintenance PreventiveMaintenance { get; set; } = null!;
        public ICollection<WorkOrderPart> UsedParts { get; set; } = new List<WorkOrderPart>();
        public ICollection<WorkOrderHistory> Histories { get; set; } = new List<WorkOrderHistory>();

    }
}
