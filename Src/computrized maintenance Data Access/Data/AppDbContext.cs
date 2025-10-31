using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.DownTimeTracking;
using computrized_maintenance_Data_Access.Entites.InventoryManagement;
using computrized_maintenance_Data_Access.Entites.preventiveMaintenanceManagement;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        public DbSet<WorkOrderPart> workOrderParts { get; set; }
        public DbSet<WorkOrderHistory> WorkOrderHistories { get; set; }
        public DbSet<InventoryItem> inventoryItems { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<PreventiveMaintenance> PreventiveMaintenances{ get; set;}
        public DbSet<DownTimeEvent> DownTimeEvents { get; set; }
    }
}
