using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
	public class AssetConfiguration : IEntityTypeConfiguration<Asset>
	{
		public void Configure(EntityTypeBuilder<Asset> builder)
		{
			builder.ToTable("Assets");
			builder.HasKey(A => A.ID)
				   .HasAnnotation("SqlServer = Identity", "(1,1)");


			builder.Property(A => A.AssetName)
				   .HasMaxLength(100)
				   .IsRequired();

			builder.Property(A => A.ManufactuerName)
					.HasMaxLength(100)
				   .IsRequired();

			builder.Property(A => A.ManufactuerModelNumber)
				   .HasMaxLength(20)
				   .IsRequired();

			builder.Property(A => A.PurchaseDate)
				   .HasColumnType("date")
				   .IsRequired();

			builder.Property(A => A.WarrantyExpiryDate)
				  .HasColumnType("date")
				  .IsRequired();

			builder.Property(A => A.InstallationDate)
				  .HasColumnType("datetime")
				  .IsRequired(false);

			builder.Property(A => A.CreateAssetDate)
				.HasColumnType("datetime")
				.IsRequired();

			builder.Property(A => A.UpdateAssetDate)
				.HasColumnType("datetime")
				.IsRequired(false);

			builder.Property(A => A.PurchaseCost)
				  .HasPrecision(18, 2)
				  .IsRequired();

			builder.Property(A => A.AssetCategoryID)
				.IsRequired();

			builder.Property(A => A.AssetLocationID)
			  .IsRequired();

			builder.Property(A => A.CreateByUser)
			  .IsRequired();

			builder.Property(A => A.AssetStatus)
				.HasConversion
				(
				x => x.ToString(),
				y => Enum.Parse<Asset_Status_Type>(y)
				);

			builder.Property(A => A.MeterReading)
			   .HasConversion
			   (
			   x => x.ToString(),
			   y => Enum.Parse<MeterReading>(y)
			   );

			builder.Property(A => A.Criticality)
			  .HasConversion
			  (
			  x => x.ToString(),
			  y => Enum.Parse<Criticality_Rating>(y)
			  );


			builder.HasOne(A => A.Location)
				   .WithOne(L => L.Asset)
				   .HasForeignKey<Asset>(x => x.AssetLocationID)
				   .IsRequired();

			builder.HasOne(A => A.Category)
				   .WithOne(C => C.Asset)
				   .HasForeignKey<Asset>(A => A.AssetCategoryID)
				   .IsRequired();

			builder.HasMany(A => A.AssetImages)
				   .WithOne(I => I.Asset)
				   .HasForeignKey(I => I.AssetID)
				   .IsRequired();



			builder.HasData(LoadData());




		}

		private List<Asset> LoadData()
		{
			return new List<Asset>()
			{
				new Asset()
				{
					  ID =1,
					  AssetName = "HVAC Unit - Floor 3",
					  AssetTagNumber = "HVAC-3F-01",
					  ManufactuerName = "Carrier",
					  ManufactuerModelNumber = "24VNA9",
					  PurchaseDate = new DateTime(2022, 3, 15),
					  PurchaseCost = 12500.00m,
					  WarrantyExpiryDate = new DateTime(2027, 3, 14),
					  InstallationDate = new DateTime(2022, 4, 1),
					  AssetCategoryID = 1,  // HVAC Equipment
					  AssetLocationID = 2, // Floor 3 Mechanical Room
					  AssetStatus = Asset_Status_Type.Active,
					  MeterReading = MeterReading.Hours,
					  Criticality = Criticality_Rating.High,
					  CreateAssetDate = new DateTime(2022, 3, 20),
					  CreateByUser = 1 // UserID
				},

				 new Asset(){

					 ID = 2,
					 AssetName = "Forklift #5",
					 AssetTagNumber = "FL-005",
					 ManufactuerName = "Toyota",
					 ManufactuerModelNumber = "8FGCU25",
					 PurchaseDate = new DateTime(2021, 8, 10),
					 PurchaseCost = 32000.00m,
					 WarrantyExpiryDate = new DateTime(2024, 8, 9),
					 InstallationDate = null, // Mobile equipment
					 AssetCategoryID = 2,  // Vehicles
					 AssetLocationID = 1, // Warehouse A
					 AssetStatus = Asset_Status_Type.UnderMaintenance,
					 MeterReading = MeterReading.Hours,
					 Criticality = Criticality_Rating.Medium,
					 CreateAssetDate = new DateTime(2021, 8, 12 ,14,30,0),
					 UpdateAssetDate = new DateTime(2023, 11, 15,20,12,0),
					  CreateByUser = 1 // UserID
				  },

				  new Asset(){
				     ID= 3,
					 AssetName = "Server Rack UPS",
					 AssetTagNumber = "IT-UPS-02",
					 ManufactuerName = "APC",
					 ManufactuerModelNumber = "SMX1500RM2U",
					 PurchaseDate = new DateTime(2023, 1, 5),
					 PurchaseCost = 2200.00m,
					 WarrantyExpiryDate = new DateTime(2026, 1, 4),
					 InstallationDate = new DateTime(2023, 1, 10),
					 AssetCategoryID = 3,  // IT Equipment
					 AssetLocationID = 3, // Data Center
					 AssetStatus = Asset_Status_Type.Active,
					 MeterReading = MeterReading.PowerCycles,
					 Criticality = Criticality_Rating.High,
					 CreateAssetDate = new DateTime(2023, 1, 6),
					 CreateByUser = 1 // UserID
				  }
			};
		}
	}
}
