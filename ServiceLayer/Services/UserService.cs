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

        public Task<CustomResponseDto<IEnumerable<UserListDto>>> GetUserList()
        {
            throw new NotImplementedException();
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
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);//;hashler
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

        public Task<CustomResponseDto<bool>> UserBiographyUpdate(UserBiographyUpdateDto userBiographyUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<bool>> UserFollow(UserFollowDto userFollowDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<bool>> UserInfoUpdate(UserInfoUpdateDto userInfoUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
    
}
