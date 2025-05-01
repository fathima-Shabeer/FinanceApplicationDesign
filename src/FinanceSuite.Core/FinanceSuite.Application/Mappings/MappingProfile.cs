// Mappings/MappingProfile.cs
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Real Estate Mappings
        CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => $"{src.Address}, {src.City} {src.PostalCode}"))
            .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.PropertyType.ToString())); // Enum to string

        CreateMap<PropertyCreateDto, Property>(); // For creating new entities

        // Add mappings for other entities and DTOs...
    }
}