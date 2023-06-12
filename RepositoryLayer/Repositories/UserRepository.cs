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
            var followingIds = _db.Follows.Where(x => x.FollowerId == id).Select(x => x.FollowingId);
            List<int> ids = new List<int>();
            foreach (var item in followingIds)
            {
                int i = Convert.ToInt32(item);

                ids.Add(i);
            }
            var users=(from u in await _db.Users.ToListAsync()
                       where !ids.Contains(u.Id) && u.Id!=id
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
