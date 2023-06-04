using CoreLayer.Entities.HelperTables;
using CoreLayer.Entities.Shared;
using CoreLayer.Enum;
using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.User
{
    public class User:Base
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }//username email adresinin @ den öncesi olcak güncellenebilecek.
        public string PhoneNumber { get; set; }
        public EGender Gender { get; set; }
        public string Biography { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IEnumerable<UserProfileImages> UserProfileImages { get; set; }
        public IEnumerable<Follow> Following { get; set; }//Takip Edilen
        public IEnumerable<Follow> Followers { get; set; }//Takip Eden
        public IEnumerable<Shared.Shared> Shareds { get; set; }
        public IEnumerable<Shared.Comment> Comments { get; set; }
        public IEnumerable<Shared.SharedLike> Likes { get; set; }
        public IEnumerable<Notification.Notification> Notifications { get; set; }
    }
}
