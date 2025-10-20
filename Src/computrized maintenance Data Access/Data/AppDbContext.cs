using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;

namespace computrized_maintenance_Data_Access.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer(ClsUtility.ConnectionString)
        //        .LogTo(Console.WriteLine,LogLevel.Information);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }


        public DbSet<Asset> Assets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories {  get; set; }
    }
}
