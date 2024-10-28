using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Dto;
using api.Utils;

namespace api.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<PagingResultDto<T>> GetAllAsync(QueryObject queryObject, Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);

        Task<T> CreateAsync(T entity);

        Task<T> FindByIdAsync(int id);

        Task CreateRangeAsync(List<T> entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync();

        Task<T> findOneByConditionAsync(Expression<Func<T, bool>> filter);

        Task<List<T>> findAllByConditionAsync(Expression<Func<T, bool>> filter);
    }
}