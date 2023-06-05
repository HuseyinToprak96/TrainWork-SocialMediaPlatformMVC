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
        public async Task<IEnumerable<User>> GetUsersNotFollow(int id)
        {
            var users=(from u in await _db.Users.ToListAsync()
                       join f in await _db.Follows.ToListAsync()
                       on u.Id equals f.FollowerId
                       into uf
                       from u_f in uf.DefaultIfEmpty()
                       where u_f==null && u.Id!= id
                       select u).ToList();
            return users;
        }

        public async Task<User> Login(string UsernameOrEmail)
        {
            var user=await _db.Users.FirstOrDefaultAsync(x=>x.Username == UsernameOrEmail || x.Email==UsernameOrEmail);
            return user;
        }
    }
}
