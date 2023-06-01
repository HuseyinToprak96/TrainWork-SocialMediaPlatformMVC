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
        T GetT(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //İstediğimiz Filtrede arama yapabiliriz.
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        bool Any(Expression<Func<T, bool>> expression);// var mı yok mu? Kontrol için
    }
}
