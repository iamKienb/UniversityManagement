using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Dto;
using api.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace api.Repository.RepositoryImp
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDBContext _context;
        public RepositoryBase(ApplicationDBContext context)
        {
            _context = context;
        }
   
        public async Task<PagingResultDto<T>> GetAllAsync(QueryObject queryObject,
        Expression<Func<T, bool>> where = null,
        params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (where != null)
            {
                query = query.Where(where);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include).AsQueryable();
                }
            }

            var totalRecords = await query.CountAsync();
            var resultItems = await query.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToListAsync();
            var pageResults = new PagingResultDto<T>(resultItems, totalRecords, queryObject.PageSize, queryObject.PageNumber);

            return pageResults;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }


        public async Task CreateRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<T> findOneByConditionAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query =  query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<T>> findAllByConditionAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query =  query.Where(filter);
            }
            return await query.ToListAsync();
        }



    }
}