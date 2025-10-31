using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.preventiveMaintenanceManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computrized_maintenance_Data_Access.Data.Config
{
    internal class PreventiveMaintenanceConfiguration : IEntityTypeConfiguration<PreventiveMaintenance>
    {
        public void Configure(EntityTypeBuilder<PreventiveMaintenance> builder)
        {
            builder.ToTable(nameof(PreventiveMaintenance)+"s","maintain");

            builder.HasKey(pm => pm.ID)
                .HasAnnotation("Sql server Identity", "1,1");

            builder.Property(x => x.TaskDescription)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.Frequency)
                .HasConversion(p => p.ToString(),
                               p => (Enumes.EnFrequencyTask)Enum.Parse(typeof(Enumes.EnFrequencyTask), p))
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.NextDueDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasOne(x => x.Asset)
                   .WithMany()
                   .HasForeignKey(x => x.AssetID)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasData(SeeData.PreventiveMaintenances);
        }
    }
}
