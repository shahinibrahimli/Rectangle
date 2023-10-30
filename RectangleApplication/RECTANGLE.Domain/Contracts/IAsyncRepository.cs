using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rectangle.Domain.Contracts
{
    public interface IAsyncRepository<T>
        where T : class
    {
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> ListAll(Func<T, bool> filter = null); 
        Task<T> Add(T entity, bool save = true);
        Task<ICollection<T>> AddRange(ICollection<T> entities, bool save = true);
        Task<bool> Update(T entity, bool save = true);
        Task<bool> UpdateRange(ICollection<T> entities, bool save = true);
        Task<bool> Delete(T entity, bool save = true);
        Task<bool> DeleteRange(ICollection<T> entities, bool save = true);
        IQueryable<T> GetQuery(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] including);
        Task<T> AddAsync(T entity);
    }
}
