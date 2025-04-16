using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
	{
		public void Configure(EntityTypeBuilder<SubCategory> builder)
		{
			builder.ToTable("SubCategories");

			builder.HasKey(S => S.ID)
				   .HasAnnotation("SqlServer = Identity", "(1,1)");

			builder.Property(S => S.Sub_Category_Name)
				.HasMaxLength(80)
				.IsRequired();

			builder.Property(S => S.CategoryID)
			  .IsRequired();


			builder.HasData(LoadData());

		}

		private List<SubCategory> LoadData()
		{
			return new List<SubCategory>
			{
						new SubCategory {ID=1 ,Sub_Category_Name = "CNC Machines", CategoryID = 1 },
						new SubCategory {ID=2 ,Sub_Category_Name = "Assembly Lines", CategoryID = 1 },
						new SubCategory {ID=3 ,Sub_Category_Name = "HVAC Systems", CategoryID = 2 },
						new SubCategory {ID=4 ,Sub_Category_Name = "Electrical Systems", CategoryID = 2 },
			};
		}
	}
}
