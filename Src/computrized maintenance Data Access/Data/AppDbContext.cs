using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.DownTimeTracking;
using computrized_maintenance_Data_Access.Entites.InventoryManagement;
using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using computrized_maintenance_Data_Access.Entites.ReportsAndAnalysis;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using Microsoft.EntityFrameworkCore;

namespace computrized_maintenance_Data_Access.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        // Eager Load Entities 
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderPart> WorkOrderParts { get; set; }
        public DbSet<WorkOrderHistory> WorkOrderHistories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<PreventiveMaintenance> PreventiveMaintenances { get; set; }
        public DbSet<DownTimeEvent> DownTimeEvents { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PMSchedule> Schedules { get; set; }
    }
}
