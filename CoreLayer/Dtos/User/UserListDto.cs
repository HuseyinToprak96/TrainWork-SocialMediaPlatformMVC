using CoreLayer.Entities.HelperTables;
using CoreLayer.Entities.User;
using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }//        public required string Name { get; set; } c# 11.0
        public string Surname { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }//username email adresinin @ den öncesi olcak güncellenebilecek.
        public string PhoneNumber { get; set; }
        public EGender Gender { get; set; }
        public string DistrictName { get; set; }
        public int DistrictId { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
    }
}
