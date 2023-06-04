using CoreLayer.Dtos.GenericDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces.Service
{
    public interface IService<T> where T : class
    {
        Task<CustomResponseDto<T>> GetAsync(int id);
        Task<CustomResponseDto<IEnumerable<T>>> GetAllAsync();
        CustomResponseDto<IQueryable<T>> Where(Expression<Func<T, bool>> expression); 
        Task<CustomResponseDto<T>> AddAsync(T entity);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(T entity);
        Task<CustomResponseDto<NoContentDto>> DeleteAsync(int id);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<T, bool>> expression);

    }
}
