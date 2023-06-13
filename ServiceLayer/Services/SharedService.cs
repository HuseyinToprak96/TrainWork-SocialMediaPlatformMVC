using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Dtos.Shared;
using CoreLayer.Entities.Shared;
using CoreLayer.Enum;
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

        public async Task<CustomResponseDto<bool>> AddLike(SharedLikeDto sharedLikeDto)
        {
            var like = _sharedLikeRepository.Where(x => x.SharedId == sharedLikeDto.SharedId && x.UserId == sharedLikeDto.UserId).FirstOrDefault();
            if (like == null)
            {
                if (await _sharedRepository.AnyAsync(x => x.Id == sharedLikeDto.SharedId && x.IsDeleted == false && x.IsActive == true) && await _userRepository.AnyAsync(x => x.Id == sharedLikeDto.UserId && x.IsDeleted == false && x.IsActive == true))
                {
                    await _sharedLikeRepository.AddAsync(new CoreLayer.Entities.Shared.SharedLike { SharedId = sharedLikeDto.SharedId, UserId = sharedLikeDto.UserId });
                    return CustomResponseDto<bool>.Success(200, true);
                }
                return CustomResponseDto<bool>.Fail(200, "Gönderi Bulunamadı!");
            }
            else
            {
                await _sharedLikeRepository.DeleteAsync(like.Id);
                return CustomResponseDto<bool>.Success(201, true);
            }
        }

        public async Task<CustomResponseDto<CommentListDto>> CommentAddDto(CommentAddDto commentAddDto)
        {
            if (await _sharedRepository.AnyAsync(x=>x.Id==commentAddDto.SharedId) && await _userRepository.AnyAsync(x=>x.Id==commentAddDto.UserId))
            {
                Comment comment = new Comment() { UserId = commentAddDto.UserId, Content = commentAddDto.Comment, SharedId = commentAddDto.SharedId , TopCommentId=commentAddDto.TopCommentId};
                await _commentRepository.AddAsync(comment);
                var user = await _userRepository.GetAsync(commentAddDto.UserId);
                CommentListDto commentListDto = new CommentListDto() { Comment = commentAddDto.Comment, CreatedDate = DateTime.Now, TopCommentId = commentAddDto.TopCommentId, UserFullName = user.Name + " " + user.Surname };
                return CustomResponseDto<CommentListDto>.Success(200, commentListDto);
            }
            return CustomResponseDto<CommentListDto>.Fail(404,"Kullanıcı Veya Paylaşım Bulunamadı!");
        }

        public Task<CustomResponseDto<CommentListDto>> CommentAnswer(CommentAnswerDto commentAnswerDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<bool>> CommentDeleteOrUpdate(CommentDeleteorUpdateDto commentDeleteorUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<IEnumerable<CommentListDto>>> GetSharedCommentList(int sharedId)
        {
            var users = await _userRepository.GetAllAsync();
            var comments = await _commentRepository.GetAllAsync();
            var result = (from c in comments
                          where c.SharedId == sharedId && c.IsDeleted== false
                          select new CommentListDto
                          {
                              Comment = c.Content,
                              CreatedDate = c.CreatedDate,
                              Id = c.Id,
                              TopCommentId = c.TopCommentId,
                              SharedId = Convert.ToInt32(c.SharedId)
                          }).OrderByDescending(x=>x.CreatedDate).AsEnumerable();
            return CustomResponseDto<IEnumerable<CommentListDto>>.Success(200, result);
        }

        public async Task<CustomResponseDto<IEnumerable<SharedListDto>>> GetSharedFilter(SharedFilterDto sharedFilterDto)
        {
            var users = await _userRepository.GetAllAsync();
            var shareds = await _sharedRepository.GetAllAsync();
            if(sharedFilterDto.sharedType>0)
            shareds = shareds.Where(x => x.Type==(EFileType)sharedFilterDto.sharedType).ToList();
            if(!string.IsNullOrWhiteSpace(sharedFilterDto.username))
            users = users.Where(x => x.Username == sharedFilterDto.username).ToList();
            var data = (from s in shareds
                        join u in users
                        on s.UserId equals u.Id
                        select new SharedListDto
                        {
                        Id = s.Id,
                        Username=u.Username,
                        Title=s.Title,
                        Description=s.Description,
                        CreatedDate=s.CreatedDate,
                        Path = s.Path,
                        Type = s.Type
                        });
            return CustomResponseDto<IEnumerable<SharedListDto>>.Success(200, data);
        }

        public async Task<CustomResponseDto<IEnumerable<SharedListDto>>> GetUserShareds(int userId)
        {
            var sharedLikeList = await _sharedLikeRepository.GetAllAsync();
            var commentList = await _commentRepository.GetAllAsync();
            var userList= await _userRepository.GetAllAsync();
            var result = (from s in await _sharedRepository.GetAllAsync()
                          join u in await _userRepository.GetAllAsync()
                          on s.UserId equals u.Id
                          where s.UserId== userId
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


        public async Task<CustomResponseDto<IEnumerable<SharedListDto>>> HomeSharedList(int userId)
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
                             CreatedDate=s.CreatedDate
                         }).OrderByDescending(x=>x.CreatedDate).ToList();
            foreach (var item in datas)
            {
                if (await _sharedLikeRepository.AnyAsync(x=>x.UserId==userId && x.SharedId==item.Id))
                    item.isLike = true;
            }
            return CustomResponseDto<IEnumerable<SharedListDto>>.Success(200,datas);
        }

        public Task<CustomResponseDto<bool>> SharedLike(SharedLikeDto sharedLikeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<bool>> SharedRepeat(int sharedId, int userId)
        {
            if (await _sharedRepository.AnyAsync(x => x.Id == sharedId && x.IsDeleted==false && x.IsActive==true) && await _userRepository.AnyAsync(x => x.Id == userId && x.IsDeleted == false && x.IsActive == true))
            {
                var shared = await _sharedRepository.GetAsync(sharedId);
                Shared newShared = new Shared { Description = shared.Description, Path = shared.Path, Title = shared.Title, Type = shared.Type, UserId = userId };
                await _sharedRepository.AddAsync(shared);
                return CustomResponseDto<bool>.Success(200, true);
            }
            else
            {
                return CustomResponseDto<bool>.Fail(200, "Bulunamadı!");
            }
        }
    }
}
