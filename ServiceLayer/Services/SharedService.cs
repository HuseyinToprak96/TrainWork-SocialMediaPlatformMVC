﻿using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Dtos.Shared;
using CoreLayer.Entities.Shared;
using CoreLayer.Interfaces.Service;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class SharedService : Service<Shared>, ISharedService
    {
        private SharedRepository _sharedRepository = new SharedRepository();
        private GenericRepository<SharedLike> _sharedLikeRepository=new GenericRepository<SharedLike>();
        private UserRepository _userRepository = new UserRepository();
        private GenericRepository<Comment> _commentRepository=new GenericRepository<Comment>();
        public Task<CustomResponseDto<CommentListDto>> CommentAddDto(CommentAddDto commentAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<CommentListDto>> CommentAnswer(CommentAnswerDto commentAnswerDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<bool>> CommentDeleteOrUpdate(CommentDeleteorUpdateDto commentDeleteorUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<IEnumerable<SharedListDto>>> GetUserShareds(int userId)
        {
            var sharedLikeList = await _sharedLikeRepository.GetAllAsync();
            var commentList = await _commentRepository.GetAllAsync();
            var userList= await _userRepository.GetAllAsync();
            var result = (from s in await _sharedRepository.GetAllAsync()
                          join u in await _userRepository.GetAllAsync()
                          on s.UserId equals u.Id
                          select new SharedListDto
                          {
                               Description = s.Description,
                                Id = s.Id,
                                 Path = s.Path,
                                  Title= s.Title,
                                  Type = s.Type,
                                   Username=u.Username,
                                    LikeCount=(from l in sharedLikeList
                                               where l.SharedId == s.Id
                                               select l).Count(),
                                     CommentList=(from cl in commentList
                                                  where cl.SharedId == s.Id
                                                  select new CommentListDto
                                                  {
                                                       Comment=cl.Content,
                                                        CreatedDate=cl.CreatedDate,
                                                         Id=cl.Id,
                                                          TopCommentId=cl.TopCommentId,
                                                           UserFullName= (from cu in userList
                                                                          where cu.Id==cl.UserId
                                                                          select cu.Username).FirstOrDefault(),
                                                  }).ToList()
                          });


            return CustomResponseDto<IEnumerable<SharedListDto>>.Success(200, result);
        }

        public async Task<CustomResponseDto<IEnumerable<SharedListDto>>> HomeSharedList()
        {
            var users=await _userRepository.GetAllAsync();
            var datas = (from s in await _sharedRepository.GetAllAsync()
                         where s.IsDeleted == false && s.IsActive == true
                         select new SharedListDto
                         {
                             Id = s.Id,
                             Description = s.Description,
                             Path = s.Path,
                             Title = s.Title,
                             LikeCount = _sharedLikeRepository.Where(x=>x.SharedId==s.Id).Count(),
                             Type = s.Type,
                             Username =users.FirstOrDefault(x=>x.Id==s.UserId).Username,
                         }).ToList();
           

            
            return CustomResponseDto<IEnumerable<SharedListDto>>.Success(200,datas);
        }

        public Task<CustomResponseDto<bool>> SharedLike(SharedLikeDto sharedLikeDto)
        {
            throw new NotImplementedException();
        }
    }
}
