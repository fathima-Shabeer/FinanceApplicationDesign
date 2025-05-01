// Interfaces/Persistence/IRepository.cs
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    // Add methods for querying (e.g., using specifications pattern)
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

// Interfaces/Persistence/IPropertyRepository.cs (Example specific repo)
public interface IPropertyRepository : IRepository<Property>
{
    Task<IEnumerable<Property>> GetPropertiesByCityAsync(string city);
}

// Interfaces/Persistence/IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    IPropertyRepository PropertyRepository { get; }
    // Add other repositories here (IInvoiceRepository, IAssetRepository...)
    Task<int> CommitAsync(); // Save changes
}

// Interfaces/Services/IRealEstateService.cs
public interface IRealEstateService
{
    Task<PropertyDto?> GetPropertyDetailsAsync(int propertyId);
    Task<IEnumerable<PropertyListDto>> GetAllPropertiesAsync();
    Task<int> CreatePropertyAsync(PropertyCreateDto propertyDto);
    // Add other service methods...
}