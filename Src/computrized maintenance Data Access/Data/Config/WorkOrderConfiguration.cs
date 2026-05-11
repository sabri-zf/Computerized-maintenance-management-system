using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{

       /// <summary>
       /// configuration to <see cref=" WorkOrder"/>  Properties
       /// </summary>
       internal sealed class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
       {
              public void Configure(EntityTypeBuilder<WorkOrder> builder)
              {

                     builder.ToTable(nameof(WorkOrder) + "s", "maintain");
                     builder.HasKey(x => x.ID)
                         .HasAnnotation("Sql server Identity", "1,1");

                     builder.Property(x => x.Description)
                         .HasMaxLength(300)
                         .IsRequired();

                     builder.Property(x => x.WorkOrderNumber)
                            .HasMaxLength(10)
                            .IsRequired();

                     builder.Property(x => x.AssetID)
                            .HasColumnType("int")
                            .IsRequired();

                     builder.Property(x => x.CreatedByID)
                         .HasColumnType("int")
                         .IsRequired();

                     builder.Property(x => x.AssignedToID)
                         .HasColumnType("int")
                         .IsRequired();

                     builder.Property(x => x.CreatedDate)
                         .HasColumnType("datetime")
                         .IsRequired();

                     builder.Property(x => x.StartDate)
                            .HasColumnType("datetime")
                            .IsRequired();

                     builder.Property(x => x.DueDate)
                            .HasColumnType("datetime")
                            .IsRequired();

                     builder.Property(x => x.CompeletedDate)
                            .HasColumnType("datetime")
                            .IsRequired(false);

                     builder.Property(x => x.Note)
                            .IsRequired(false);

                     builder.Property(x => x.Status)
                            .HasConversion(
                                 s => s.ToString(),
                                 v => (En_workOrderStatus)Enum.Parse(typeof(En_workOrderStatus), v)
                                          )
                            .IsRequired();

                     builder.Property(x => x.Type)
                            .HasConversion(
                                   s => s.ToString(),
                                   v => (En_MaintenaceType)Enum.Parse(typeof(En_MaintenaceType), v)
                            )
                            .IsRequired();

                     builder.Property(x => x.Priority)
                          .HasConversion(
                                 s => s.ToString(),
                                 v => (En_Priority)Enum.Parse(typeof(En_Priority), v)
                          )
                          .IsRequired();

            builder.Property(x => x.PreventiveMaintenanceID)
                            .HasColumnType("int")
                            .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);


                     // here we make relationchip with workeordes and asset (1(A)-M(Wo))
                     builder.HasOne(w => w.Asset)
                            .WithMany(a => a.WorkOrders)
                            .HasForeignKey(x => x.AssetID)
                            .IsRequired();


                     // bulid relationship with workOrder and workOrderPart
                     builder.HasMany(x => x.UsedParts)
                            .WithOne(x => x.WorkOrder)
                            .HasForeignKey(x => x.WO_ID)
                            .IsRequired();

                     builder.HasMany(x => x.Histories)
                            .WithOne(x => x.WorkOrder)
                            .HasForeignKey(x => x.WO_ID)
                            .IsRequired();

                     builder.HasOne(x => x.PreventiveMaintenance)
                            .WithMany(p => p.WorkOrders)
                            .HasForeignKey(x => x.PreventiveMaintenanceID)
                            .IsRequired();

                     builder.HasData(SeeData.WorkOrders);
              }



       }
}
