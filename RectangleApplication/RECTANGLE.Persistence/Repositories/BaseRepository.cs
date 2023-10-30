using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rectangle.Domain.Contracts;

namespace Rectangle.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T>
        where T : class
    {
        protected readonly RectangleDbContext _dbContext;
        private readonly IAuditContext _auditContext;

        public BaseRepository(RectangleDbContext dbContext, IAuditContext auditContext)
        {
            this._dbContext = dbContext;
            _auditContext = auditContext;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await this._dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAll(Func<T, bool> filter = null)
        {
            List<T> query = await this._dbContext.Set<T>().ToListAsync();

            if (filter != null)
            {
                query = query.Where(filter).ToList();
            }

            return query;
        }
         
        public virtual IQueryable<T> FormulateQuery(string orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = this._dbContext.Set<T>();

            if (includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, includeProperty) => current.Include<T, object>(includeProperty));
            }

            return !string.IsNullOrEmpty(orderBy) ? queryable.OrderBy(orderBy).AsQueryable() : queryable;
        }

        public virtual async Task<T> Add(T entity, bool save = true)
        {
            await this._dbContext.Set<T>().AddAsync(entity);
            if (save)
            {
                await _auditContext.LogAndSaveAsync();
            }

            return entity;
        }

        public virtual async Task<ICollection<T>> AddRange(ICollection<T> entities, bool save = true)
        {
            await this._dbContext.Set<T>().AddRangeAsync(entities);
            if (save)
            {
                await _auditContext.LogAndSaveAsync();
            }

            return entities;
        }

        public virtual async Task<bool> Update(T entity, bool save = true)
        {
            this._dbContext.Update<T>(entity);
            return save ? await _auditContext.LogAndSaveAsync() > 0 : true;
        }

        public virtual async Task<bool> UpdateRange(ICollection<T> entities, bool save = true)
        {
            this._dbContext.UpdateRange(entities);
            return save ? await _auditContext.LogAndSaveAsync() > 0 : true;
        }

        public virtual async Task<bool> Delete(T entity, bool save = true)
        {
            this._dbContext.Set<T>().Remove(entity);
            return save ? await _auditContext.LogAndSaveAsync() > 0 : true;
        }

        public virtual async Task<bool> DeleteRange(ICollection<T> entities, bool save = true)
        {
            this._dbContext.Set<T>().RemoveRange(entities);
            return save ? await _auditContext.LogAndSaveAsync() > 0 : true;
        }

        public virtual async Task<int> Count() => await this._dbContext.Set<T>().CountAsync();

        public virtual IQueryable<T> GetQuery(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = this._dbContext.Set<T>();

            if (includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, includeProperty) => current.Include<T, object>(includeProperty));
            }

            return queryable;
        } 
        protected virtual IQueryable<T> ApplySearchToQuery(IQueryable<T> query, string search)
        {
            return query;
        }
         

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] including)
        {
            var enetities = _dbContext.Set<T>().AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null) enetities = enetities.Include(include);
                });

            return enetities;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _auditContext.LogAndSaveAsync();

            return entity;
        }
    }
}
