// Persistence/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
// Ensure the correct namespace is included at the top of the file


using System.Reflection; // Needed for ApplyConfigurationsFromAssembly

public class ApplicationDbContext : DbContext // Or IdentityDbContext if using Identity
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<Lease> Leases { get; set; } = null!;
    public DbSet<Mortgage> Mortgages { get; set; } = null!;
    // Add DbSets for Finance and TradeWorkingCapital entities...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Scans assembly for all IEntityTypeConfiguration implementations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // Optional: Override SaveChangesAsync to update AuditableEntity properties
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Add logic here to set CreatedDate, LastModifiedDate etc.
        // Example: Iterate through ChangeTracker.Entries<AuditableEntity>()
        return await base.SaveChangesAsync(cancellationToken);
    }
}

public class Mortgage
{
}

public class Lease
{
}