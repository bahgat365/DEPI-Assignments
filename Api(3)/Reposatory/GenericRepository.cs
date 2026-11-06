using System.Linq.Expressions;
using DEPI_ECommerce_API.Data;
using DEPI_ECommerce_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEPI_ECommerce_API.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ECommerceDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(ECommerceDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default) =>
        await DbSet.FindAsync(new object?[] { id }, cancellationToken);

    public Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        DbSet.AnyAsync(predicate, cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        await DbSet.AddAsync(entity, cancellationToken);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Context.SaveChangesAsync(cancellationToken);

    public IQueryable<T> Query() => DbSet.AsQueryable();
}
