using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Interfaces.Service;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private GenericRepository<T> _genericRepository=new GenericRepository<T>();
        public async Task<CustomResponseDto<T>> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            return CustomResponseDto<T>.Success(200,entity);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return CustomResponseDto<bool>.Success(200,await _genericRepository.AnyAsync(expression));
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(int id)
        {
            await _genericRepository.DeleteAsync(id);
            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<CustomResponseDto<IEnumerable<T>>> GetAllAsync()
        {
            return CustomResponseDto<IEnumerable<T>>.Success(200, await _genericRepository.GetAllAsync());
        }

        public async Task<CustomResponseDto<T>> GetAsync(int id)
        {
            return CustomResponseDto<T>.Success(200, await _genericRepository.GetAsync(id));
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(T entity)
        {
            await _genericRepository.UpdateAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public CustomResponseDto<IQueryable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return CustomResponseDto<IQueryable<T>>.Success(200,_genericRepository.Where(expression));
        }
    }
}
