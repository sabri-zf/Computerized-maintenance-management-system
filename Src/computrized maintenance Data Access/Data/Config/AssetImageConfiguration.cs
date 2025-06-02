using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    public class AssetImageConfiguration : IEntityTypeConfiguration<AssetImage>
    {
        public void Configure(EntityTypeBuilder<AssetImage> builder)
        {
            builder.ToTable("AssetImages");
            builder.HasKey(I => I.ID)
                .HasAnnotation("SqlServer: identity", "(1,1)");

            builder.Property(i => i.ImagePath)
                .IsRequired();

            builder.Property(i => i.ImageWidth)
                .IsRequired();

            builder.Property(i => i.ImageHeight)
              .IsRequired();


            builder.HasData(LoadData());
        }

        private List<AssetImage> LoadData()
        {
            return new List<AssetImage>
            {
                new AssetImage {ID  = 1 ,ImagePath = "C:\\User\\Samsung\\Images\\imageOne1.png", ImageWidth=150,ImageHeight=150,  AssetID =1 },
                new AssetImage {ID = 2 ,ImagePath = "C:\\User\\Samsung\\Images\\imageOne2.png", ImageWidth=150,ImageHeight=150,  AssetID =1 },
                new AssetImage {ID = 3 ,ImagePath = "C:\\User\\Samsung\\Images\\imageTwo1.png", ImageWidth=150,ImageHeight=150,  AssetID =2 },
                new AssetImage {ID = 4 ,ImagePath = "C:\\User\\Samsung\\Images\\imageTwo2.png", ImageWidth=150,ImageHeight=150,  AssetID =2 },
                new AssetImage {ID = 5 ,ImagePath = "C:\\User\\Samsung\\Images\\imageThree1.png", ImageWidth=150,ImageHeight=150,  AssetID =3 },
                new AssetImage {ID = 6 ,ImagePath = "C:\\User\\Samsung\\Images\\imageThree2.png", ImageWidth=150,ImageHeight=150,  AssetID =3 }
            };
        }
    }
}
