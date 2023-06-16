using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer;
using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Dtos.User;
using CoreLayer.Entities.HelperTables;
using CoreLayer.Entities.User;
using CoreLayer.Interfaces.Service;
using RepositoryLayer.Repositories;
using ServiceLayer.Hash;

namespace ServiceLayer.Services
{
    public class UserService:Service<User>,IUserService
    {
        private UserRepository _userRepository = new UserRepository();
        private GenericRepository<Role> _roleRepository= new GenericRepository<Role>();
        private GenericRepository<District> _districtRepository=new GenericRepository<District>();
        private GenericRepository<City> _cityRepository =new GenericRepository<City>();
        private GenericRepository<UserProfileImages> _userProfileImageRepository=new GenericRepository<UserProfileImages>();
        private GenericRepository<Follow> _followRepository=new GenericRepository<Follow>();
        public Task<CustomResponseDto<bool>> AddProfileImage(UserAddorUpdateProfileImageDto userAddorUpdateProfileImageDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<UserListDto>> AddUser(CreateUserDto createUser)
    {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(createUser.Password, out passwordHash, out passwordSalt);//;hashler
            User user= new User { Email= createUser.Email, PasswordHash=passwordHash, PasswordSalt=passwordSalt, Username = createUser.Email.Split('@')[0], PhoneNumber=createUser.PhoneNumber };
            await _userRepository.AddAsync(user);
            return CustomResponseDto<UserListDto>.Success(200,new UserListDto { Id=user.Id, Email=user.Email, Username=user.Username, PhoneNumber = createUser.PhoneNumber  });
        }

        public Task<CustomResponseDto<IEnumerable<UserListDto>>> FilterUsers(UserSearchFilterDto userSearchFilterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<IEnumerable<UserListDto>>> GetFollowers(int userId)
        {
            var followers= _followRepository.Where(x => x.FollowingId == userId).AsEnumerable();
            var data = (from f in followers
                        join u in await _userRepository.GetAllAsync()
                        on f.FollowerId equals u.Id
                      select new UserListDto
                      {
                           Id = u.Id,
                            Name= u.Name,
                             Surname= u.Surname,
                              Username= u.Username
                      }).ToList();
            return CustomResponseDto<IEnumerable<UserListDto>>.Success(200, data);
        }

        public CustomResponseDto<int> GetFollowersCount(int userId)
        {
            var followersCount = _followRepository.Where(x => x.FollowingId == userId).Count();
            return CustomResponseDto<int>.Success(200,followersCount);
        }
        public CustomResponseDto<int> GetFollowingsCount(int userId)
        {
            var followersCount = _followRepository.Where(x => x.FollowerId == userId).Count();
            return CustomResponseDto<int>.Success(200, followersCount);
        }
        public async Task<CustomResponseDto<IEnumerable<UserListDto>>> GetFollowings(int userId)//Takip edilenler
        {
            var followers = _followRepository.Where(x => x.FollowerId == userId).ToList();
            var data = (from f in followers
                        join u in await _userRepository.GetAllAsync()
                        on f.FollowingId equals u.Id
                        select new UserListDto
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Surname = u.Surname,
                            Username = u.Username
                        });
            return CustomResponseDto<IEnumerable<UserListDto>>.Success(200, data);
        }

        public async Task<CustomResponseDto<UserInfoUpdateDto>> GetInfo(int id)
        {
            var data = (from u in await _userRepository.GetAllAsync()
                        where u.Id == id
                        select new UserInfoUpdateDto { UserId = u.Id, DistrictId = Convert.ToInt32(u.DistrictId), Email = u.Email, Gender = u.Gender, Name = u.Name, PhoneNumber = u.PhoneNumber, Surname = u.Surname, Username = u.Username }).FirstOrDefault();
            data.ImagePath = (from i in await _userProfileImageRepository.GetAllAsync()
                              where i.UserId==id && i.IsMain==true
                              select i.Path
                           ).FirstOrDefault();
            return CustomResponseDto<UserInfoUpdateDto>.Success(200, data);
        }

        public async Task<CustomResponseDto<IEnumerable<RecommendedPeopleDto>>> GetRecommendedPeople(int userId)
        {
            var users = await _userRepository.GetUsersNotFollow(userId);

            List<RecommendedPeopleDto> list=new List<RecommendedPeopleDto>();
            foreach (var item in users)
            {
                var data = _userProfileImageRepository.Where(x => x.UserId == item.Id && x.IsMain == true).FirstOrDefault();
                RecommendedPeopleDto recommendedPeopleDto=new RecommendedPeopleDto { Id=item.Id, Username=item.Username, Image=data.Path};
                list.Add(recommendedPeopleDto);
            }
            return CustomResponseDto<IEnumerable<RecommendedPeopleDto>>.Success(200,list);
        }

        public async Task<CustomResponseDto<UserBiographyUpdateDto>> GetUserBiography(int userId)
        {
            var data = await _userRepository.GetAsync(userId);
            return CustomResponseDto<UserBiographyUpdateDto>.Success(200,new UserBiographyUpdateDto { UserId=data.Id, Biography=data.Biography});
        }

        public Task<CustomResponseDto<IEnumerable<UserListDto>>> GetUserList()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<IEnumerable<UserPhotoDto>>> GetUserPhotos(int userId)
        {
            var result = (from i in await _userProfileImageRepository.GetAllAsync()
                          where i.UserId==userId
                          select new UserPhotoDto
                          {
                             Id=i.Id,
                              CreatedDate=i.CreatedDate,
                               Description=i.Description,
                                IsMain=i.IsMain,
                                 Path= i.Path
                          });
            return CustomResponseDto<IEnumerable<UserPhotoDto>>.Success(200, result);
        }

        public async Task<CustomResponseDto<UserListDto>> Login(LoginDto loginDto)
        {
            var user=await _userRepository.Login(loginDto.Email);
            if (HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                var userListDto = (from u in await _userRepository.GetAllAsync()
                                   where u.Id==user.Id
                                   select new UserListDto
                                   {
                                       Id = u.Id,
                                       Email = u.Email,
                                       Username = u.Username,
                                       PhoneNumber = u.PhoneNumber,
                                       Gender = u.Gender,
                                       Name = u.Name,
                                       Surname = u.Surname,
                                       RoleId=Convert.ToInt32(u.RoleId)
                                   }).FirstOrDefault();
                var data = (from d in await _districtRepository.GetAllAsync()
                            join c in await _cityRepository.GetAllAsync()
                            on d.CityId equals c.Id
                            select new
                            {
                                DistrictId = d.Id,
                                DistrictName = d.Name,
                                CityId = c.Id,
                                CityName = c.Name,
                            }).FirstOrDefault();
                if (data != null)
                {
                    userListDto.DistrictId = data.DistrictId;
                    userListDto.DistrictName = data.DistrictName;
                    userListDto.CityId = data.CityId;
                    userListDto.CityName = data.CityName;
                }
                userListDto.RoleName=(from r in await _roleRepository.GetAllAsync()
                                      where r.Id == user.RoleId select r.Name).FirstOrDefault(); 
                return CustomResponseDto<UserListDto>.Success(200, userListDto);
            }
            return CustomResponseDto<UserListDto>.Fail(404, "User Not Found");
        }

        public async Task<CustomResponseDto<bool>> RemoveFollowerOrFollowingRemove(int followerId, int followingId)
        {
            var data=_followRepository.Where(x=>x.FollowerId==followerId && x.FollowingId==followingId).FirstOrDefault();
            if (data != null)
            {
                await _followRepository.DeleteAsync(data.Id);
                return CustomResponseDto<bool>.Success(200, true);
            }
            return CustomResponseDto<bool>.Fail(404, "Bulunamadı!");
           
        }

        public async Task<CustomResponseDto<bool>> UpdatePassword(string password, int userId)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user=await _userRepository.GetAsync(userId);
            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;
            await _userRepository.UpdateAsync(user);
            return CustomResponseDto<bool>.Success(200, true);
        }

        public Task<CustomResponseDto<bool>> UpdateProfileImage(UserAddorUpdateProfileImageDto userAddorUpdateProfileImageDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<bool>> UpdateProfilePhoto(int userId, int photoId)
        {
            if (await _userProfileImageRepository.AnyAsync(x => x.Id == photoId) && await _userRepository.AnyAsync(x => x.Id == userId))
            {
                var images = _userProfileImageRepository.Where(x => x.UserId == userId);
                foreach (var image in images.ToList())
                {
                    if (image.Id != photoId)
                        image.IsMain = false;
                    else
                        image.IsMain = true;
                    await _userProfileImageRepository.UpdateAsync(image);
                }
                return CustomResponseDto<bool>.Success(200, true);
            }
            else
            {
                return CustomResponseDto<bool>.Fail(404, "Bulunamadı!");
            }
        }

        public async Task<CustomResponseDto<bool>> UserBiographyUpdate(UserBiographyUpdateDto userBiographyUpdateDto)
        {
            var user= await _userRepository.GetAsync(userBiographyUpdateDto.UserId);
            user.Biography=userBiographyUpdateDto.Biography;
            await _userRepository.UpdateAsync(user);
            return CustomResponseDto<bool>.Success(200,true);
        }

        public async Task<CustomResponseDto<bool>> UserFollow(UserFollowDto userFollowDto)
        {
            if (await _userRepository.AnyAsync(x=>x.Id==userFollowDto.FollowerId && x.IsDeleted==false && x.IsActive==true) && await _userRepository.AnyAsync(x=>x.Id==userFollowDto.FollowingId && x.IsDeleted == false && x.IsActive == true))
            {
                Follow follow = new Follow { FollowerId = userFollowDto.FollowerId, FollowingId = userFollowDto.FollowingId };
                await _followRepository.AddAsync(follow);
                return CustomResponseDto<bool>.Success(200, true);
            }
            return CustomResponseDto<bool>.Fail(404, "Kullanıcı bulunamadı!");
        }

        public async Task<CustomResponseDto<bool>> UserInfoUpdate(UserInfoUpdateDto userInfoUpdateDto)
        {
            var user=await _userRepository.GetAsync(userInfoUpdateDto.UserId);
            if (user != null)
            {
                user.Surname = userInfoUpdateDto.Surname;
                user.Name = userInfoUpdateDto.Name;
                user.Email = userInfoUpdateDto.Email;
                user.Username = userInfoUpdateDto.Username;
                user.PhoneNumber = userInfoUpdateDto.PhoneNumber;
                await _userRepository.UpdateAsync(user);
                if (!String.IsNullOrWhiteSpace(userInfoUpdateDto.ImagePath))
                {
                    var images = _userProfileImageRepository.Where(x => x.UserId == userInfoUpdateDto.UserId);
                    foreach (var image in images.ToList())
                    {
                        image.IsMain = false;
                        await _userProfileImageRepository.UpdateAsync(image);
                    }
                    if (userInfoUpdateDto.ImagePath != "")
                    {
                        UserProfileImages userProfileImages = new UserProfileImages { IsMain = true, UserId = userInfoUpdateDto.UserId, Path = userInfoUpdateDto.ImagePath, CreatedDate = DateTime.Now };
                        await _userProfileImageRepository.AddAsync(userProfileImages);
                    }
                }
                return CustomResponseDto<bool>.Success(200, true);
            }
            return CustomResponseDto<bool>.Fail(404,"User Not Found!");
        }

        public async Task<CustomResponseDto<IEnumerable<int>>> GetIdUserFollowers(int id)
        {
            if (await _userRepository.AnyAsync(x => x.Id == id))
            {
                var users = await _userRepository.GetUserFollowers(id);
                var datas = users.Select(x => x.Id);
                return CustomResponseDto<IEnumerable<int>>.Success(200, datas);
            }
            return CustomResponseDto<IEnumerable<int>>.Fail(404, "Kullanıcı Bulunamadı!");
        }
    }
    
}
