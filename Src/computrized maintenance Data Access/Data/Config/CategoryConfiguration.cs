using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("AssetCategories");

            builder.Property(C => C.Category_Name)
                .HasColumnName("CategoryName")
                .IsRequired();

            builder.HasMany(C => C.SubCategories)
                   .WithOne(S => S.Category)
                   .HasForeignKey(C => C.CategoryID)
                   .IsRequired();


            builder.HasData(LoadData());
        }

        private List<Category> LoadData()
        {

            return new List<Category>
{
    new Category(){ID = 1  ,Category_Name = "Production Equipment"},
    new Category(){ID = 2  ,Category_Name = "Facility Infrastructure" },
    new Category(){ID = 3  ,Category_Name = "Transportation" },
    new Category(){ID = 4  ,Category_Name = "IT Infrastructure" },
    new Category(){ID = 5  ,Category_Name = "Safety Equipment" },
    new Category(){ID = 6  ,Category_Name = "Tools" },
    new Category(){ID = 7  ,Category_Name = "Mechanical" },
    new Category(){ID = 8  ,Category_Name = "Electrical" },
    new Category(){ID = 9  ,Category_Name = "Electronic" },
    new Category(){ID = 10 ,Category_Name = "Hydraulic" },
    new Category(){ID = 11 ,Category_Name = "Pneumatic" },
    new Category(){ID = 12 ,Category_Name = "Process Equipment" },
    new Category(){ID = 13 ,Category_Name = "Support Equipment" },
    new Category(){ID = 14 ,Category_Name = "Standby Equipment" },
    new Category(){ID = 15 ,Category_Name = "Fire Protection" },
    new Category(){ID = 16 ,Category_Name = "Office Equipment" },
    new Category(){ID = 17 ,Category_Name = "Medical Equipment" },
    new Category(){ID = 18 ,Category_Name = "Laboratory Equipment" },
    new Category(){ID = 19 ,Category_Name = "Construction Equipment" }
            };
        }
    }
}
