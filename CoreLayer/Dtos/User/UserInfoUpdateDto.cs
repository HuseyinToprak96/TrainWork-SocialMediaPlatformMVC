using CoreLayer.Entities.HelperTables;
using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserInfoUpdateDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public EGender Gender { get; set; }
        public int DistrictId { get; set; }
        public string ImagePath { get; set; }
    }
}
