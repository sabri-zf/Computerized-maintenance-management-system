using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.DownTimeTracking;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Data.Config
{
    internal class DownTimeEventConfiguration : IEntityTypeConfiguration<DownTimeEvent>
    {
        public void Configure(EntityTypeBuilder<DownTimeEvent> builder)
        {
            builder.ToTable(nameof(DownTimeEvent)+"s","report");
            builder.HasKey(x => x.ID)
                .HasAnnotation("Sql server identity", "1,1")
                .IsClustered();


            builder.Property(x => x.ID)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(x => x.AssetID)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(x => x.WO_ID)
                   .HasColumnOrder(2)
                   .IsRequired(false);

            builder.Property(x => x.StartDownTimeEvent)
                   .HasColumnType("datetime2")
                   .HasColumnOrder(3)
                   .IsRequired();

            builder.Property(x => x.EndDownTimeEvent)
                   .HasColumnType("datetime2")
                   .HasColumnOrder(4)
                   .IsRequired(false);

            builder.Property(x => x.DownTimeType)
                   .HasConversion(
                                v => v.ToString(),
                                T => (En_DownTimeType)Enum.Parse(typeof(En_DownTimeType), T))
                   .HasMaxLength(20)
                   .HasColumnOrder(5)
                   .IsRequired();

            builder.Property(x => x.Reason)
                   .HasMaxLength(200)
                   .HasColumnOrder(6)
                   .IsRequired();

            builder.Property(x => x.ActionTaken)
                   .HasMaxLength(200)
                   .HasColumnOrder(7)
                   .IsRequired(false);

            builder.Property(x => x.CreateAt)
                   .HasColumnOrder (8)
                   .IsRequired();

            builder.Property(x => x.PerformedByID)
                   .HasColumnOrder(9)
                   .IsRequired();

 
            //Relationship with Entities
            builder.HasOne(x => x.Asset)
                   .WithMany()
                   .HasForeignKey(x => x.AssetID)
                   .IsRequired();

            builder.HasOne(x => x.WorkOrder)
                   .WithMany()
                   .HasForeignKey(x => x.WO_ID)
                   .IsRequired(false);

            builder.HasData(SeeData.downTimeEvents);
        }
    }
}
