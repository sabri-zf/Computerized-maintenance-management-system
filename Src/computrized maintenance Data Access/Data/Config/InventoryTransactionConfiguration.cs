using computrized_maintenance_Data_Access.Data.Seed;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Transactions;

namespace computrized_maintenance_Data_Access.Data.Config
{
    /// <summary>
    ///  /// configuration to <see cref=" InventoryTransaction"/> Properties
    /// </summary>
    internal sealed class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable(nameof(InventoryTransaction) + "s", "Inventory");

            builder.HasKey(x => x.ID)
                .HasAnnotation("Sql Server Identity", "1,1");

            builder.Property(x => x.InventoryItemID)
                   .IsRequired();

            builder.Property(x => x.Type)
                    .HasConversion(
                    s => s.ToString(),
                    v => (En_TransactionType)Enum.Parse(typeof(En_TransactionType), v)
                    )
                    .IsRequired();

            builder.Property(x => x.Quntity)
                   .IsRequired();

            builder.Property(x => x.TransactionDate)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(x => x.Reference)
                   .HasMaxLength(60)
                   .IsRequired(false);

            builder.HasData(SeeData.InventoryTransactions);
        }
    }
}
