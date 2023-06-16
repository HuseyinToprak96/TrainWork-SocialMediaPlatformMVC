using CoreLayer.Interfaces;
using RepositoryLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _db;
        private DbSet<T> _dbSet;
        public GenericRepository()
        {
            _db = new AppDbContext();
            _dbSet=_db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _db.SaveChangesAsync() ;
            _db.Dispose();
        }

        public async Task AddRangeAsync(IEnumerable<T> values)
        {
            _dbSet.AddRange(values);
            await _db.SaveChangesAsync() ;
            _db.Dispose();

        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task DeleteAsync(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            await _db.SaveChangesAsync();
            _db.Dispose();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            var data= await _db.SaveChangesAsync();
            _db.Dispose();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
