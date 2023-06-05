using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Dtos.User;
using CoreLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces.Service
{
    public interface IUserService:IService<User>
    {
        Task<CustomResponseDto<UserListDto>> Login(LoginDto loginDto);
        Task<CustomResponseDto<UserListDto>> AddUser(CreateUserDto userDto);
        Task<CustomResponseDto<bool>> AddProfileImage(UserAddorUpdateProfileImageDto userAddorUpdateProfileImageDto);
        Task<CustomResponseDto<bool>> UpdateProfileImage(UserAddorUpdateProfileImageDto userAddorUpdateProfileImageDto);
        Task<CustomResponseDto<bool>> UserBiographyUpdate(UserBiographyUpdateDto userBiographyUpdateDto);
        Task<CustomResponseDto<UserInfoUpdateDto>> GetInfo(int id);
        Task<CustomResponseDto<bool>> UserFollow(UserFollowDto userFollowDto);
        Task<CustomResponseDto<bool>> UserInfoUpdate(UserInfoUpdateDto userInfoUpdateDto);
        Task<CustomResponseDto<IEnumerable<UserListDto>>> GetUserList();
        Task<CustomResponseDto<IEnumerable<UserListDto>>> FilterUsers(UserSearchFilterDto userSearchFilterDto);
        Task<CustomResponseDto<IEnumerable<RecommendedPeopleDto>>> GetRecommendedPeople(int userId);
    }
}
