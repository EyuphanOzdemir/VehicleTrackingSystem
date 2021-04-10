using CoreEntity;
using CoreEntity.RepositoryInterfaces;
using DBAccessLibrary.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessLibrary.Repositories
{
    /// <summary>
    ///  This is the DB-kind implementation of IGenericRepository.
    ///  It supports CRUD methods, searching, pagination and sorting.
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly DBContext _context;

        public GenericRepository(DBContext context)
        {
            _context = context;
        }

        public DBContext GetContext => _context;
        public async Task<T> AddAsync(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            return (await SaveContext()) ? result.Entity : null;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            return await SaveContext();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(string sortColumn = "Id")
        {
            return await _context.Set<T>().OrderBy(t => EF.Property<object>(t, sortColumn)).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncPaginated(int page, int pageSize, string sortColumn = "Id")
        {
            return await _context.Set<T>().OrderBy(t => EF.Property<object>(t, sortColumn)).Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<V>> GetViewRecords<V>(string sortColumn = "Id") where V : BaseEntity
        {
            return await _context.Set<V>().OrderBy(t => EF.Property<object>(t, sortColumn)).ToListAsync();
        }

        public async Task<IEnumerable<V>> GetViewRecordsPaginated<V>(int page, int pageSize, string sortColumn = "Id") where V : BaseEntity
        {
            return await _context.Set<V>().OrderBy(t => EF.Property<object>(t, sortColumn)).Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<V> GetViewRecordById<V>(int Id) where V : BaseEntity
        {
            return await _context.Set<V>().FindAsync(Id);
        }
        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await SaveContext();
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }

        private async Task<bool> SaveContext()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int CountForView<V>() where V: BaseEntity
        {
            return _context.Set<V>().Count(); 
        }
    }
}
