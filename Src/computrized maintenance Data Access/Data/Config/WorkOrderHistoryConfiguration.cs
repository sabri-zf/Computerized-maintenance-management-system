using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    /// <summary>
    ///  /// configuration to <see cref=" WorkOrderHistory"/> Properties
    /// </summary>
    internal sealed class WorkOrderHistoryConfiguration : IEntityTypeConfiguration<WorkOrderHistory>
            {
                public void Configure(EntityTypeBuilder<WorkOrderHistory> builder)
                {

                    builder.ToTable("WorkOrderHisties", "maintain");
                    builder.HasKey(x => x.ID)
                        .HasAnnotation("Sql server Identity", "1,1");

                    builder.Property(x => x.WO_ID)
                           .IsRequired();

                    builder.Property(x => x.ActionDate)
                           .HasColumnType("datetime2")
                           .IsRequired();

                    builder.Property(x => x.PerformedActionByID)
                           .HasColumnType ("int")
                           .IsRequired();

                    builder.Property(x => x.Action)
                           .HasConversion(
                        s => s.ToString(),
                        v => (WorkOrderHistoryActionStatus)Enum.Parse(typeof(WorkOrderHistoryActionStatus), v))
                          .IsRequired();


                    //relationship
                    // workOrder history has one Worke Order, however workOrder belang to One WorkOrderHistory
                    builder.HasOne(x => x.WorkOrder)
                           .WithMany(x => x.Histories)
                           .HasForeignKey(x => x.WO_ID)
                           .IsRequired();
                           

                    builder.HasData(SeeData.WorkOrderHistorys);
                }
            }
}
