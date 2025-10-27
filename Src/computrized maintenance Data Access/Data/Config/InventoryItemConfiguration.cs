using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    /// <summary>
    ///  /// configuration to <see cref=" InventoryItem"/> Properties
    /// </summary>
    internal sealed class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
           builder.ToTable(nameof(InventoryItem)+"s","Inventory");

            builder.HasKey(x => x.ID)
                .HasAnnotation("Sql server Identity", "1,1");

            builder.Property(x => x.ItemName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.PartNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.Quintity)
                   .IsRequired();

            builder.Property(x => x.ReorderLevel)
                   .IsRequired();

            builder.Property(x => x.UnitCost)
                   .HasPrecision(18,2)
                   .IsRequired();

            builder.Property(x => x.LocationID)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasColumnType("bit")
                   .IsRequired();

            // Relationships

            builder.HasOne(x => x.Location)
                   .WithMany()
                   .HasForeignKey(x => x.LocationID)
                   .IsRequired();

            builder.HasMany(x => x.Transactions)
                   .WithOne(x => x.InventoryItem)
                   .HasForeignKey(x => x.InventoryItemID)
                   .IsRequired();



            builder.HasData(SeeData.InventoryItems);
            
        }
    }
}
