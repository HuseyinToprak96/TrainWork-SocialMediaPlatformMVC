using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.User
{
    public class Follow
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public User Follower { get; set; }//Takip Eden
        public User Following { get; set; }//Takip Edilen
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
