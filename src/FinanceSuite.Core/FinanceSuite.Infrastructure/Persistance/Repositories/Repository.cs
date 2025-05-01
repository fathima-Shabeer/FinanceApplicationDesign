// Persistence/Repositories/Repository.cs
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        // No SaveChangesAsync here - UnitOfWork handles it
        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        // EF Core tracks changes, just mark state if detached
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}

// Persistence/Repositories/PropertyRepository.cs
public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    // Cast DbContext for specific queries if needed
    private ApplicationDbContext AppDbContext => (ApplicationDbContext)_dbContext;

    public PropertyRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Property>> GetPropertiesByCityAsync(string city)
    {
        return await AppDbContext.Properties
                                 .Where(p => p.City == city)
                                 .ToListAsync();
    }
}