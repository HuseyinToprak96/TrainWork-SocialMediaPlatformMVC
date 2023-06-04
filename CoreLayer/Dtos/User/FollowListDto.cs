using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class FollowListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Gt { get; set; }//geri takip ediyorsa true etmiyorsa false döner
    }
}
