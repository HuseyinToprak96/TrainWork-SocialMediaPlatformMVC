using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //İstediğimiz Filtrede arama yapabiliriz.
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> values);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);// var mı yok mu? Kontrol için
    }
}
