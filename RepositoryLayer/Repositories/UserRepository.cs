using CoreLayer.Entities.User;
using CoreLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public async Task<User> Login(string UsernameOrEmail)
        {
            var user=await _db.Users.FirstOrDefaultAsync(x=>x.Username == UsernameOrEmail || x.Email==UsernameOrEmail);
            return user;
        }
    }
}
