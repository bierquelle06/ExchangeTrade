using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();
        
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, 
            int pageSize, 
            Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetPagedReponseAsync(Expression<Func<T, bool>> predicate = null,
            string includeProperties = "",
            int pageNumber = 1,
            int pageSize = 10,
            bool hasPagination = true,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null);

        Task<T> AddAsync(T entity);
        Task<List<T>> AddAllAsync(List<T> entities);

        Task<T> UpdateAsync(T entity);
        Task<T> UpdateAsync(decimal id, T entity);
        Task<List<T>> UpdateAllAsync(List<T> entities);

        Task DeleteAsync(T entity);
        Task DeleteAllAsync(List<T> entity);
        Task DeleteSoftAsync(int id);
    }
}
