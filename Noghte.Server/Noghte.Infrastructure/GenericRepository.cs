using Microsoft.EntityFrameworkCore;
using Noghte.BuildingBlock.Common;
using Noghte.BuildingBlock.Utilities;
using Noghte.Domain;
using Noghte.Infrastructure.ApplicationDbContext;

namespace Noghte.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly NoghteDbContext _dbContext;

        public DbSet<TEntity> Entities { get; }

        public virtual IQueryable<TEntity> Table => Entities;

        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public GenericRepository(NoghteDbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<TEntity>();
        }

        #region Async Methods

        public async Task<TEntity?> GetNullableByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        }
        
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            var result = await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
