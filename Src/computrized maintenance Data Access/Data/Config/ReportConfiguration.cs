using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.ReportsAndAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Data.Config
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.ID)
                .HasAnnotation("Sql server identity", ("1,1"))
                .IsClustered();

            builder.Property(x => x.MTTR)
                .HasColumnName("Mean_Time_TO_Repair")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.MTBF)
                 .HasColumnName("Mean_Time_between_Failure")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Availability)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.MDT)
                .HasColumnName("Mean_DownTime")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.AssetID)
                .IsRequired();

            builder.Property(x => x.StartPeriod)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.EndPeriod)
               .HasColumnType("datetime")
               .IsRequired();

            builder.Property(x => x.CreateAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.Interval_Running_machine)
                .IsRequired();

            builder.HasOne(x => x.Asset)
                .WithMany()
                .HasForeignKey(x => x.AssetID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasData(SeeData.Reports);
        }
    }
}
