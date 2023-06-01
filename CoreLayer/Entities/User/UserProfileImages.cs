using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.User
{
    public class UserProfileImages
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsMain { get; set; }//Görünen Profil resmi değiştirilirse kullanıcının önce true olan fotosu false edilecek sonra yeni ana pp si true olcak.
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
