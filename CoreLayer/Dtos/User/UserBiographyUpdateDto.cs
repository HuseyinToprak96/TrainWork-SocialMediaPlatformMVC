using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserBiographyUpdateDto
    {
        public int UserId { get; set; }
        public string Biography { get; set; }
    }
}
