// Persistence/Configuration/PropertyConfiguration.cs
using FinanceSuite.Core.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.PurchasePrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.CurrentValue)
            .HasColumnType("decimal(18,2)");

        // Define relationships
        builder.HasMany(p => p.Leases)
               .WithOne(l => l.Property)
               .HasForeignKey(l => l.PropertyId);
    }
}