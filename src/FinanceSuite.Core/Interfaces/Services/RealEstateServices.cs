// Services/RealEstateService.cs
using AutoMapper; // Inject AutoMapper
using FinanceSuite.Core.Interfaces.Persistence;
using FinanceSuite.Core.Interfaces.Services;
using FinanceSuite.Application.DTOs.RealEstate;
using FinanceSuite.Core.Entities.RealEstate; // Import entity namespace

public class RealEstateService : IRealEstateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    // Inject logging, other services if needed (e.g., IUserService for CreatedBy)

    public RealEstateService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PropertyDto?> GetPropertyDetailsAsync(int propertyId)
    {
        var property = await _unitOfWork.PropertyRepository.GetByIdAsync(propertyId);
        if (property == null) return null;

        // Add business logic if needed (e.g., calculate profitability)

        return _mapper.Map<PropertyDto>(property);
    }

    public async Task<int> CreatePropertyAsync(PropertyCreateDto propertyDto)
    {
        // Validation can be done here or via filters/FluentValidation in Web layer
        var property = _mapper.Map<Property>(propertyDto);

        // Set auditable properties (could be done in DbContext SaveChanges override too)
        property.CreatedDate = DateTime.UtcNow;
        property.LastModifiedDate = DateTime.UtcNow;
        // property.CreatedBy = _userService.GetCurrentUserId(); // Example

        await _unitOfWork.PropertyRepository.AddAsync(property);
        await _unitOfWork.CommitAsync();

        return property.Id;
    }

    public async Task<IEnumerable<PropertyListDto>> GetAllPropertiesAsync() // Assuming PropertyListDto exists
    {
        var properties = await _unitOfWork.PropertyRepository.ListAllAsync();
        // Add any filtering/sorting logic
        return _mapper.Map<IEnumerable<PropertyListDto>>(properties);
    }

    // Implement other service methods...
}