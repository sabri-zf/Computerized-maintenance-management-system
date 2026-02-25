using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    internal class PreventiveMaintenanceConfiguration : IEntityTypeConfiguration<PreventiveMaintenance>
    {
        public void Configure(EntityTypeBuilder<PreventiveMaintenance> builder)
        {
            builder.ToTable(nameof(PreventiveMaintenance) + "s", "maintain");

            builder.HasKey(pm => pm.ID)
                .HasAnnotation("Sql server Identity", "1,1");

            builder.Property(x => x.TaskDescription)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.Frequency)
                .HasConversion(p => p.ToString(),
                               p => (Enumes.En_FrequencyTask)Enum.Parse(typeof(Enumes.En_FrequencyTask), p))
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);

            // builder.Property(x => x.NextDueDate)
            //     .HasColumnType("datetime2")
            //     .IsRequired();

            builder.HasOne(x => x.Asset)
                    .WithMany()
                    .HasForeignKey(x => x.AssetID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

            builder.HasData(SeeData.PreventiveMaintenances);
        }
    }
}
