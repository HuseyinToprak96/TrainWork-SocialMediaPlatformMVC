using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserAddorUpdateProfileImageDto
    {
        public int UserId { get; set; }
        public string Image { get; set; }
        public int ImageId { get; set; }
    }
}
