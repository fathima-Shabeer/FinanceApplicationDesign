// ViewModels/RealEstate/PropertyViewModel.cs
using System.ComponentModel.DataAnnotations;
// Ensure the correct namespace is included at the top of the file
// Ensure the correct namespace is included at the top of the file


public class PropertyViewModel
{
    public int Id { get; set; }
    [Display(Name = "Address")]
    public string FullAddress { get; set; } = string.Empty;
    [Display(Name = "Type")]
    public string PropertyType { get; set; } = string.Empty;
    [Display(Name = "Current Value")]
    [DataType(DataType.Currency)]
    public decimal CurrentValue { get; set; }
    // Add other properties needed by the view
}

// ViewModels/RealEstate/PropertyCreateViewModel.cs
public class PropertyCreateViewModel
{
    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Property Type")]
    public PropertyType PropertyType { get; set; } // Use Enum

    [Required]
    [Range(0, double.MaxValue)]
    [Display(Name = "Purchase Price")]
    [DataType(DataType.Currency)]
    public decimal PurchasePrice { get; set; }

    [Required]
    [Display(Name = "Purchase Date")]
    [DataType(DataType.Date)]
    public DateTime PurchaseDate { get; set; } = DateTime.Today;
}