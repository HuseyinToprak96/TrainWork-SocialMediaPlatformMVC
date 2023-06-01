using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer;
using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Dtos.User;
using CoreLayer.Entities.User;
using ServiceLayer.Hash;

namespace ServiceLayer.Services
{
    public class UserService
    {
    public void AddUser(CreateUserDto createUser)
    {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(createUser.Password, out passwordHash, out passwordSalt);//;hashler

        }
        public async Task<CustomResponseDto<User>> Login(LoginDto loginDto)
        {
            User user = new User();//kişiler tablosunda maili olan varmı aranacak varsa 
            if(HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            return CustomResponseDto<User>.Success(200,user);
            return CustomResponseDto<User>.Fail(404, "User Not Found");
        }
    }
    
}
