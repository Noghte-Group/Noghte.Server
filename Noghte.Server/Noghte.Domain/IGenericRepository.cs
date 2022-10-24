using Microsoft.EntityFrameworkCore;

namespace Noghte.Domain
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    }
}
