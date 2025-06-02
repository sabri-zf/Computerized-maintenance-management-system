using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");
            builder.HasKey(L => L.ID)
                .HasAnnotation("SqlServer: identity","(1,1)");

            builder.Property(L => L.LocationName)
                .HasMaxLength(50)
                .IsRequired();


            builder.HasData(LoadData());
            
        }

        private List<Location> LoadData()
        {
            return new List<Location>
            {
                new Location {ID = 1, LocationName = "Building A" },
                new Location {ID = 2, LocationName = "Building B" },
                new Location {ID = 3, LocationName = "facility - 109" }
            };
        }
    }
}
