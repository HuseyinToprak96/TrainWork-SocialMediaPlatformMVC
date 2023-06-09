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

        public async Task<CustomResponseDto<UserInfoUpdateDto>> GetInfo(int id)
        {
            var data = (from u in await _userRepository.GetAllAsync()
                        select new UserInfoUpdateDto { UserId = u.Id, DistrictId = Convert.ToInt32(u.DistrictId), Email = u.Email, Gender = u.Gender, Name = u.Name, PhoneNumber = u.PhoneNumber, Surname = u.Surname, Username = u.Username }).FirstOrDefault();
            data.ImagePath = (from i in await _userProfileImageRepository.GetAllAsync()
                              where i.UserId==id && i.IsMain==true
                              select i.Path
                           ).FirstOrDefault();
            return CustomResponseDto<UserInfoUpdateDto>.Success(200, data);
        }

        public async Task<CustomResponseDto<IEnumerable<RecommendedPeopleDto>>> GetRecommendedPeople(int userId)
        {
            var images = await _userProfileImageRepository.GetAllAsync();
            var users = await _userRepository.GetUsersNotFollow(userId);
            var data = (from u in users
           join img in images
           on u.Id equals img.UserId into u_img
           from ui in u_img.DefaultIfEmpty()
           where u.Id!=userId
                        select new RecommendedPeopleDto
                        {
                             Id=u.Id,
                             Username=u.Username,
                             Image=ui?.Path
                              
                        }).ToList();
            return CustomResponseDto<IEnumerable<RecommendedPeopleDto>>.Success(200,data);
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
                                   select new UserListDto
                                   {
                                       Id = u.Id,
                                       Email = u.Email,
                                       Username = u.Username,
                                       PhoneNumber = u.PhoneNumber,
                                       Gender = u.Gender,
                                       Name = u.Name,
                                       Surname = u.Surname
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
            var images = _userProfileImageRepository.Where(x => x.UserId == userId);
            foreach (var image in images.ToList())
            {
                if(image.Id!=photoId)
                image.IsMain = false;
                else
                    image.IsMain = true;
                await _userProfileImageRepository.UpdateAsync(image);
            }
            return CustomResponseDto<bool>.Success(200, true);
        }

        public async Task<CustomResponseDto<bool>> UserBiographyUpdate(UserBiographyUpdateDto userBiographyUpdateDto)
        {
            var user= await _userRepository.GetAsync(userBiographyUpdateDto.UserId);
            user.Biography=userBiographyUpdateDto.Biography;
            await _userRepository.UpdateAsync(user);
            return CustomResponseDto<bool>.Success(200,true);
        }

        public Task<CustomResponseDto<bool>> UserFollow(UserFollowDto userFollowDto)
        {
            throw new NotImplementedException();
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
                if (userInfoUpdateDto.ImagePath!="")
                {
                   var images= _userProfileImageRepository.Where(x => x.UserId == userInfoUpdateDto.UserId);
                    foreach (var image in images.ToList()) 
                    {
                        image.IsMain = false;
                        await _userProfileImageRepository.UpdateAsync(image);
                    }
                    UserProfileImages userProfileImages=new UserProfileImages { IsMain=true , UserId=userInfoUpdateDto.UserId, Path=userInfoUpdateDto.ImagePath, CreatedDate=DateTime.Now};
                    await _userProfileImageRepository.AddAsync(userProfileImages);
                }
                return CustomResponseDto<bool>.Success(200, true);
            }
            return CustomResponseDto<bool>.Fail(404,"User Not Found!");
        }
    }
    
}
