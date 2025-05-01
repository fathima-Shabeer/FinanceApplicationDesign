// Persistence/Repositories/UnitOfWork.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IPropertyRepository? _propertyRepository;
    // Add private fields for other repositories

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Lazy loading repositories
    public IPropertyRepository PropertyRepository => _propertyRepository ??= new PropertyRepository(_context);
    // Implement getters for other repositories similarly

    public async Task<int> CommitAsync()
    {
        // Add logic here for dispatching domain events if using Domain-Driven Design
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}