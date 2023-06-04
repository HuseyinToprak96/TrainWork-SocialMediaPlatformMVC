using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserFollowDto
    {
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
    }
}
