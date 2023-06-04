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
                                   join r in await _roleRepository.GetAllAsync()
                                   on u.RoleId equals r.Id
                                   join d in await _districtRepository.GetAllAsync()
                                   on u.DistrictId equals d.Id
                                   join c in await _cityRepository.GetAllAsync()
                                   on d.CityId equals c.Id
                                   select new UserListDto
                                   {
                                       Id = u.Id,
                                       Email = u.Email,
                                       Username = u.Username,
                                       PhoneNumber = u.PhoneNumber,
                                       Gender = u.Gender,
                                       Name = u.Name,
                                       Surname = u.Surname,
                                       RoleName = r.Name,
                                       DistrictName=d.Name,
                                       CityName=c.Name,
                                   }).FirstOrDefault();
                return CustomResponseDto<UserListDto>.Success(200, userListDto);
            }
            return CustomResponseDto<UserListDto>.Fail(404, "User Not Found");
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
