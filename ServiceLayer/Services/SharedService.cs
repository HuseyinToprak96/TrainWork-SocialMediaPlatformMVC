using CoreLayer.Dtos.GenericDtos;
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

        public async Task<CustomResponseDto<IEnumerable<SharedListDto>>> HomeSharedList()
        {
            var data = (from s in await _sharedRepository.GetAllAsync()
                        where s.IsDeleted == false && s.IsActive == true
                        select new SharedListDto
                        {
                            Id = s.Id,
                            Description = s.Description,
                            Path = s.Path,
                            Title = s.Title,
                             LikeCount=(from sl in _sharedLikeRepository.GetAllAsync().Result
                                        where sl.SharedId==s.Id
                                        select sl.Id).Count(),
                              Username=_userRepository.GetAsync(Convert.ToInt32(s.UserId)).Result.Username,
                               CommentList=(from c in  _commentRepository.GetAllAsync().Result
                                            where c.SharedId==s.Id
                                            select new CommentListDto
                                            {
                                                 Id=c.Id,
                                                  Comment=c.Content,
                                                   CreatedDate=c.CreatedDate,
                                                    TopCommentId=c.TopCommentId,
                                                     UserFullName= _userRepository.GetAsync(Convert.ToInt32(c.UserId)).Result.Username,
                                            }).ToList()
                             
                        }).ToList();
            
            return CustomResponseDto<IEnumerable<SharedListDto>>.Success(200,data);
        }

        public Task<CustomResponseDto<bool>> SharedLike(SharedLikeDto sharedLikeDto)
        {
            throw new NotImplementedException();
        }
    }
}
