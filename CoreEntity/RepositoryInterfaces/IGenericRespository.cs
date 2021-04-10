using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreEntity.RepositoryInterfaces
{
    /// <summary>
    ///  This interface offers a set of common methods that can be used with repositories.
    ///  Since this is an interface, it can work with DB, text-based, in-memory entities etc.
    ///  GetView... methods can be used to work with Views in a RDBMS
    /// </summary>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(string sortColumn="Id");
        Task<IEnumerable<T>> GetAllAsyncPaginated(int page, int pageSize, string sortColumn = "Id");
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        bool Exists(int Id);
        public Task<IEnumerable<V>> GetViewRecords<V>(string sortColumn = "Id") where V : BaseEntity;
        Task<IEnumerable<V>> GetViewRecordsPaginated<V>(int page, int pageSize, string sortColumn = "Id") where V : BaseEntity;
        public Task<V> GetViewRecordById<V>(int Id) where V : BaseEntity;
        public int Count();
        public int CountForView<V>() where V:BaseEntity;
    }
}
