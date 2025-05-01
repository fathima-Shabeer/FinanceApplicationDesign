// DTOs/RealEstate/PropertyDto.cs
using System.ComponentModel.DataAnnotations;

public class PropertyDto
{
    public int Id { get; set; }
    public string FullAddress { get; set; } = string.Empty; // Example combined field
    public string PropertyType { get; set; } = string.Empty; // Use string for presentation
    public decimal CurrentValue { get; set; }
}

// DTOs/RealEstate/PropertyCreateDto.cs
public class PropertyCreateDto
{
    // Use validation attributes (or FluentValidation)
    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    [Required]
    public PropertyType PropertyType { get; set; } // Use Enum here
    [Range(0, double.MaxValue)]
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}