namespace computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement
{
    public class PMSchedule
    {
        public int ScheduleID { get; set; }
        public int PmID { get; set; }
        public DateTime NextDueDate { get; set; }
        public DateTime? LastCompletionDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public PreventiveMaintenance PreventiveMaintenance { get; set; } = null!;
    }
}
