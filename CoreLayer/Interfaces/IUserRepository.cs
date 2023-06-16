using CoreLayer.Entities;
using CoreLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> Login(string UsernameOrEmail);//Buradan dönen byte[] password ile girilen password kıyaslanacak
        Task<IEnumerable<User>> GetUsersNotFollow(int id);
        Task<IEnumerable<User>> GetUserFollowers(int id);
    }
}
