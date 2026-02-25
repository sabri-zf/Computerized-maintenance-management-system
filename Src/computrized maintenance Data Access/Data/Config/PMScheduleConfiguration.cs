using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Data.Config
{
    internal class PMScheduleConfiguration : IEntityTypeConfiguration<PMSchedule>
    {
        public void Configure(EntityTypeBuilder<PMSchedule> builder)
        {
            builder.ToTable("Schedules", "maintain");
            builder.HasKey(s => s.ScheduleID)
                .HasAnnotation("SqlServer Identity", "1,1")
                .HasName("PK_Schedules")
                .IsClustered(true);


            builder.Property(s => s.PmID)
                .HasColumnName("PM_Id")
                .IsRequired();

            builder.Property(s => s.NextDueDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(s => s.CreatedDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(s => s.LastCompletionDate)
                .HasColumnType("datetime")
                .IsRequired(false);

                builder.Property(s => s.IsActive)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(s => s.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.HasOne(s => s.PreventiveMaintenance)
            .WithMany(p => p.PMSchedules)
            .HasForeignKey(s => s.PmID)
            .HasConstraintName("FK_Schedules_PreventiveMaintenance")
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(s => s.PmID)
                .HasDatabaseName("IX_Schedules_PM_Id");

            builder.HasIndex(s => s.NextDueDate)
                .HasDatabaseName("IX_Schedules_NextDueDate");

            builder.HasData(Seed.SeeData.PMSchedules);
        }
    }
}
