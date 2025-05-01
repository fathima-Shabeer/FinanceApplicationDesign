// Entities/Common/BaseEntity.cs
// Add the appropriate namespace for the Mortgage class
public abstract class BaseEntity
{
    public int Id { get; protected set; }
}

// Entities/Common/AuditableEntity.cs
public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; } // User ID or Name
    public DateTime LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
}

// Entities/RealEstate/Property.cs
public class Property : AuditableEntity
{
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal CurrentValue { get; set; }
    // Navigation Properties
    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    public virtual ICollection<Mortgage> Mortgages { get; set; } = new List<Mortgage>();
}

// Enums/RealEstate/PropertyType.cs
public enum PropertyType
{
    Residential,
    Commercial,
    Industrial,
    Land
}