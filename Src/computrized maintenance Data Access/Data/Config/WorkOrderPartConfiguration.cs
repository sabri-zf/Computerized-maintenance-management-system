using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    /// <summary>
    /// configuration to <see cref=" WorkOrderPart"/> Properties
    /// </summary>
    internal sealed class WorkOrderPartConfiguration : IEntityTypeConfiguration<WorkOrderPart>
        {
            public void Configure(EntityTypeBuilder<WorkOrderPart> builder)
            {
                builder.ToTable(nameof(WorkOrderPart) + "s", "maintain");
                builder.HasKey(x => x.ID)
                    .HasAnnotation("Sql server Identity", "1,1");

                builder.Property(x => x.WO_ID)
                       .HasColumnType("int")
                       .IsRequired();

                builder.Property(x => x.PartItemID)
                    .HasColumnType("int")
                    .IsRequired();

                builder.Property(x => x.QuantityUsed)
                       .IsRequired();


                //Relationship between InventoryItem with workOrderParts
                builder.HasOne(x => x.InventoryItem)
                       .WithMany(x => x.WorkOrderParts)
                       .HasForeignKey(x => x.PartItemID)
                       .IsRequired();


                builder.HasData(SeeData.WorkOrderParts);

            }


        }
}
