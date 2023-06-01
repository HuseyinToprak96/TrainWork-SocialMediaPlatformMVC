using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Shared
{
    public class SharedLike
    {
        public int Id { get; set; }
        public int? SharedId { get; set; }
        public Shared Shared { get; set; }
        public int? UserId { get; set; }
        public User.User User { get; set; }
    }
}
