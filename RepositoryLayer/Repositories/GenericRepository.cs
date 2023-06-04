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
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _db;
        private DbSet<T> _dbSet;
        public GenericRepository()
        {
            _db = new AppDbContext();
            _dbSet=_db.Set<T>();
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetT(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
