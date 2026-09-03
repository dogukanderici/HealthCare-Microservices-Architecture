using HealthCare.Descriptions.Application.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<ICollection<T>> GetAllAsync(DBQueryOptions<T>? queryOptions = null);
        IQueryable<T> GetQuearble(DBQueryOptions<T>? queryOptions = null);
        Task<T> GetByIdAsync(DBQueryOptions<T> queryOptions);
        Task<Guid> CreateAsync(T entity);
        Task<DateTimeOffset> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}