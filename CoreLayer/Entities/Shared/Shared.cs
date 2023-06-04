using CoreLayer.Entities.User;
using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Shared
{
    public class Shared:Base
    {
        public string Title { get; set; } 
        public string Description { get; set; }
        public string Path { get; set; }
        public int? UserId { get; set; }
        public User.User User { get; set; }
        public EFileType Type { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<SharedLike> Likes { get; set; }
        //Yayınla butonu işaretli ise isActive true değilse false olacak şekilde ayarlanacak. Yayınla denilen gönderiler anasayfada yayınlancak.
    }
}
