using Application.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var query = _dbContext.Set<T>().Where(predicate);

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {
            return await _dbContext
                .Set<T>()
                .Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(Expression<Func<T, bool>> predicate = null,
            string includeProperties = "",
            int pageNumber = 1, 
            int pageSize = 10,
            bool hasPagination = true,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if(includeProperties.Contains(","))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(includeProperties))
                    query = query.Include(includeProperties);
            }

            if (hasPagination)
            {
                query = query.Skip((pageNumber - 1) * pageSize);
            }

            if (OrderBy != null)
            {
                return await OrderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;

            await _dbContext.Set<T>().AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? entity : null;
        }

        public async Task<List<T>> AddAllAsync(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? entities : null;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? entity : null;
        }

        public async Task<T> UpdateAsync(decimal id, T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry<T>((await _dbContext.Set<T>().FindAsync(id))).CurrentValues.SetValues(entity);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? entity : null;
        }

        public async Task<List<T>> UpdateAllAsync(List<T> entities)
        {
            _dbContext.UpdateRange(entities);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0 ? entities : null;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(List<T> entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<T>().RemoveRange(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public async Task DeleteSoftAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry(entity).Property("IsDelete").IsModified = true;
            _dbContext.Entry(entity).Property("DeleteDate").IsModified = true;

            //Set Values
            _dbContext.Entry(entity).Property("IsDelete").CurrentValue = true;
            _dbContext.Entry(entity).Property("DeleteDate").CurrentValue = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}
